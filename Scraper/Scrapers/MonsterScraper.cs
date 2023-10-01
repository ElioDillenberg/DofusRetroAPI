using System.Text;
using System.Text.RegularExpressions;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterNameDto;
using DofusRetroClassLibrary.Enums.Localization;
using Newtonsoft.Json;
using PuppeteerSharp;

namespace ScraperDofusRetroAPI.Scrapers;

public class MonsterScraper
{
    private IBrowser _browser = null!;
    private const string EntryUrlFr = "https://solomonk.fr/fr/monstres/chercher";
    private const string EntryUrlEn = "https://solomonk.fr/en/monsters/search";
    private const string EntryUrlEs = "https://solomonk.fr/es/monstruos/buscar";

    private HttpClient _httpClient;

    public MonsterScraper(bool headless)
    {
        LaunchOptions options = new LaunchOptions
        {
            Headless = headless,
            ExecutablePath = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
            // ExecutablePath = @"/Applications/Google Chrome.app/Contents/MacOS/Google Chrome"
        };
        
        Puppeteer
            .LaunchAsync(options)
            .ContinueWith(t => _browser = t.Result)
            .GetAwaiter()
            .GetResult();

        _httpClient = new HttpClient();
    }
    
    public async Task ScrapeMonsters()
    {
        // Open new page
        IPage page = await _browser.NewPageAsync();
        
        // Go to entry url
        await page.GoToAsync(EntryUrlFr);

        // Scroll to the bottom of the page
        // await ScrollDownABit(page);
        await ScrollToBottom(page);

        // Retrieve an IEnumerable of the elements holding the data for each monster
        IElementHandle[] monsterDataElements = await ExtractMonsterDataElements(page);
        
        // Go through the list and send all NormalMonsters to the API
        foreach (IElementHandle monsterDataElement in monsterDataElements)
            await AddMonsterToApi(monsterDataElement);
        
        // Update monster with monster relations
        foreach (IElementHandle monsterDataElement in monsterDataElements)
            await UpdateMonstersWithRelatedMonsters(monsterDataElement);
        
        // AddMonster Characteristics to the API
        foreach (IElementHandle monsterDataElement in monsterDataElements)
            await AddMonsterCharacteristicsToApi(monsterDataElement);
        
        // Go through the list again and send all the Names in french to the API
        foreach (IElementHandle monsterDataElement in monsterDataElements)
            await AddNameToTheApi(monsterDataElement, Language.FR);
        
        // Now change the language to english and send all the Names in english to the API
        await page.GoToAsync(EntryUrlEn);
        // await ScrollDownABit(page);
        await ScrollToBottom(page);
        monsterDataElements = await ExtractMonsterDataElements(page);
        foreach (IElementHandle monsterDataElement in monsterDataElements)
            await AddNameToTheApi(monsterDataElement, Language.EN);
        
        // Now change the language to spanish and send all the names in spanish to the API
        await page.GoToAsync(EntryUrlEs);
        // await ScrollDownABit(page);
        await ScrollToBottom(page);
        monsterDataElements = await ExtractMonsterDataElements(page);
        foreach (IElementHandle monsterDataElement in monsterDataElements)
            await AddNameToTheApi(monsterDataElement, Language.ES);
        
        // Close
        await _browser.CloseAsync();
    }

    //
    // Names
    //
    
    private async Task AddNameToTheApi(IElementHandle monsterDataElement, Language language)
    {
        // Get Monster Id
        int monsterId = await GetMonsterId(monsterDataElement);
        if (monsterId == -1)
            Console.WriteLine("Something went wrong getting a monster Id...");
        
        // Get wrapper element for Name
        IElementHandle monsterTitleElement = await monsterDataElement.QuerySelectorAsync("a.text-sololightbeige");
        
        // Get Name
        string name = await monsterTitleElement.EvaluateFunctionAsync<string>("element => element.innerText");
        
        AddMonsterNameDto addMonsterNameDto = new AddMonsterNameDto(
            MonsterId: monsterId,
            LanguageId: (int)language,
            Name: name
        );
        
        string jsonToSend = JsonConvert.SerializeObject(addMonsterNameDto);
        Console.WriteLine(jsonToSend);
        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/Monster/Name", stringContent);
        Console.WriteLine($"Post request status code = {response.StatusCode}");
    }
    
    //
    // Characteristics
    //

    private async Task AddMonsterCharacteristicsToApi(IElementHandle monsterDataElement)
    {
        // Get Monster Id
        int monsterId = await GetMonsterId(monsterDataElement);
        if (monsterId == -1)
            Console.WriteLine("Something went wrong getting a monster Id...");
        
        // Get wrapper element for Level and RankSelector
        IElementHandle monsterTitleElement = await monsterDataElement.QuerySelectorAsync("div.row.card-solo-monster-title");
        
        // AnchorElements holding the Levels
        IElementHandle[] rankButtons = await monsterTitleElement.QuerySelectorAllAsync("div.border-solobeige-hover");
        Console.WriteLine(rankButtons.Length);
        
        // Level Element
        IElementHandle levelElement = await monsterTitleElement.QuerySelectorAsync("span");
        
        // Monster CharacteristicsWrapper
        IElementHandle[] monsterCharacteristicElements = await monsterDataElement.QuerySelectorAllAsync("li");

        foreach (IElementHandle rankButton in rankButtons)
        {
            int[] characValues = new int[monsterCharacteristicElements.Length];
            
            // Click the rank button
            await rankButton.ClickAsync();

            // Get Rank
            string stringRank = await rankButton.EvaluateFunctionAsync<string>("element => element.innerText");
            int rank = int.Parse(stringRank);
            
            // Get Level
            string stringLevel = await levelElement.EvaluateFunctionAsync<string>("element => element.innerText");
            int level = int.Parse(stringLevel);
            
            // Get Monster Characteristics
            for (int i = 0; i < monsterCharacteristicElements.Length; i++)
            {
                int? characValue = await ExtractCharacValueFromCharacElement(monsterCharacteristicElements[i]);
                if (characValue == null)
                {
                    characValue = 0;
                    Console.WriteLine("There was an issue extracting a charac value... Setting it to 0!");
                }
                characValues[i] = characValue.Value;
            }

            AddMonsterCharacteristicDto addMonsterCharacteristicDto = new AddMonsterCharacteristicDto(
                Rank: rank,
                MonsterId: monsterId,
                Level: level,
                HealthPoints: characValues[0],
                ActionPoints: characValues[1],
                MovementPoints: characValues[2],
                Experience: characValues[3],
                Initiative: characValues[4],
                Strength: characValues[5],
                Intelligence: characValues[6],
                Chance: characValues[7],
                Agility: characValues[8],
                ActionPointAvoidance: characValues[9],
                MovementPointAvoidance: characValues[10],
                NeutralResistancePercentage: characValues[11],
                EarthResistancePercentage: characValues[12],
                FireResistancePercentage: characValues[13],
                WaterResistancePercentage: characValues[14],
                AirResistancePercentage: characValues[15]
            );
            
            string jsonToSend = JsonConvert.SerializeObject(addMonsterCharacteristicDto);
            Console.WriteLine(jsonToSend);
            StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/Monster/Characteristic", stringContent);
            Console.WriteLine($"Post request status code = {response.StatusCode}");
        }
    }

    private async Task<int?> ExtractCharacValueFromCharacElement(IElementHandle elementHandle)
    {
        // Get the inner text of the element
        string innerText = await elementHandle.EvaluateFunctionAsync<string>("el => el.innerText");

        // Use regular expression to extract numeric portion (numbers or percentages)
        Match match = Regex.Match(innerText, @"(\d+)%?");

        if (match.Success && int.TryParse(match.Groups[1].Value, out int numericValue))
        {
            return numericValue;
        }
        return null; // Return null if no valid numeric value found
    }

    private async Task AddMonsterToApi(IElementHandle monsterDataElement)
    {
        // Get Monster Id
        int monsterId = await GetMonsterId(monsterDataElement);
        if (monsterId == -1)
            Console.WriteLine("Something went wrong getting a monster Id...");
            
        // Get wrapper element for Ecosystem and Breed
        IElementHandle smallElement = await monsterDataElement.QuerySelectorAsync("small.col.pl-2");
        
        // AnchorElements holding the Ecosystem and Breed
        IElementHandle[] anchorElements = await smallElement.QuerySelectorAllAsync("a");

        // Get Ecosystem Id
        int ecosystemId = await GetEcosystemId(anchorElements[0]);
        if (ecosystemId == -1)
            Console.WriteLine("Something went wrong getting an ecosystem Id...");
        
        // Get Breed Id
        int breedId = await GetBreedId(anchorElements[1]);
        if (breedId == -1)
            Console.WriteLine("Something went wrong getting a breed Id...");

        AddMonsterDto addMonsterDto = new AddMonsterDto(
            monsterId,
            ecosystemId,
            breedId
        );
        
        string jsonToSend = JsonConvert.SerializeObject(addMonsterDto);
        Console.WriteLine(jsonToSend);
        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/Monster", stringContent);
        Console.WriteLine($"Post request status code = {response.StatusCode}");
    }

    private async Task UpdateMonstersWithRelatedMonsters(IElementHandle mobDataElement) 
    {
        // Get Monster Id
        int monsterId = await GetMonsterId(mobDataElement);
        if (monsterId == -1)
            Console.WriteLine("Something went wrong getting a monster Id...");
        
        // Get related Monster Id
        int? relatedMonsterId = await GetRelatedMonsterId(mobDataElement);
        if (monsterId == -1)
            Console.WriteLine("Something went wrong getting a related monster Id...");
        
        if (relatedMonsterId == null)
        {
            Console.WriteLine("No related Monster found!");
            return;
        }
        
        UpdateMonsterDto updateMonsterDto = new UpdateMonsterDto(
            monsterId,
            relatedMonsterId.Value
        );
        
        string jsonToSend = JsonConvert.SerializeObject(updateMonsterDto);
        Console.WriteLine(jsonToSend);
        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5067/api/v1/Monster", stringContent);
        Console.WriteLine($"Put request status code = {response.StatusCode}");
    }

    private async Task<int> GetEcosystemId(IElementHandle anchorElement)
    {
        return await ExtractIdFromAnchorElement(anchorElement);
    }
    
    private async Task<int> GetBreedId(IElementHandle anchorElement)
    {
        return await ExtractIdFromAnchorElement(anchorElement);
    }

    private async Task<int> GetMonsterId(IElementHandle mobDataElement)
    {
        IElementHandle anchorElement = await mobDataElement.QuerySelectorAsync("a.text-sololightbeige");
        
        return await ExtractIdFromAnchorElement(anchorElement);
    }

    private async Task<int?> GetRelatedMonsterId(IElementHandle mobDataElement)
    {
        IElementHandle monsterTitleElement = await mobDataElement.QuerySelectorAsync("div.col-auto.card-solo-monster-title");
        
        IElementHandle? anchorElement = await monsterTitleElement.QuerySelectorAsync("a.text-solobrown");
        if (anchorElement == null)
            return null;
        
        return await ExtractIdFromAnchorElement(anchorElement);
    }
    

    private async Task<int> ExtractIdFromAnchorElement(IElementHandle anchorElement)
    {
        string href = await anchorElement.EvaluateFunctionAsync<string>("element => element.href");

        string? stringId = ExtractIdFromHref(href); 

        if (stringId == null)
            Console.WriteLine("Something went wrong...");
        else if (int.TryParse(stringId, out int id))
            return id;
        return -1;
    }

    private string? ExtractIdFromHref(string href)
    {
        // Regular expression to extract the numeric ID from the href
        Regex regex = new Regex(@"/(\d+)/");

        // Match the regular expression against the href
        Match match = regex.Match(href);

        if (match.Success && match.Groups.Count > 1)
        {
            // The numeric ID is in the second group
            return match.Groups[1].Value;
        }
        return null;
    }

    private async Task ScrollDownABit(IPage page)
    {
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
        
        await page.WaitForTimeoutAsync(500);
        
        await page.Mouse.WheelAsync(0, 2000);
    }

    private async Task ScrollToBottom(IPage page)
    {
        int totalScrolls = 150;
        for (int i = 0; i < totalScrolls; i++)
        {
            await page.Mouse.WheelAsync(0, 8000);
            await page.WaitForTimeoutAsync(500);
            Console.WriteLine($"Scroll {i + 1}/{totalScrolls}");
        }
    }
    
    private async Task<IElementHandle[]> ExtractMonsterDataElements(IPage page)
    {
        // Selector for the elements you want to extract
        string selector = ".container-fluid.mt-4[data-mob='']";

        // Query for the elements using the selector
        IElementHandle[] elements = await page.QuerySelectorAllAsync(selector);
        return elements;
    }
    
    private async Task<string> ExtractMobName(IElementHandle mobDataElement)
    {
        // Selector for the element to extract
        string selector = ".text-sololightbeige";

        // Query for the element using the selector
        IElementHandle element = await mobDataElement.QuerySelectorAsync(selector);
        return await element.EvaluateFunctionAsync<string>("element => element.innerText");
    }
}