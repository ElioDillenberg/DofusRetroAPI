using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Drop;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Monsters.MonsterDto;
using ClassLibrary.Enums.Languages;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;
using Newtonsoft.Json;
using PuppeteerSharp;

namespace DataHarvester.Scrapers;

public class MonsterScraper : BaseScraper
{
    private const string EntryUrlFr = "https://solomonk.fr/fr/monstres/chercher";
    private const string EntryUrlEn = "https://solomonk.fr/en/monsters/search";
    private const string EntryUrlEs = "https://solomonk.fr/es/monstruos/buscar";
    
    public MonsterScraper(bool headless) : base(headless) { }
    
    public override async Task Scrape()
    {
        // Open new page
        IPage page = await _browser.NewPageAsync();
        
        // Go to entry url
        await page.GoToAsync(EntryUrlFr);
        
        // Accept cookies
        await AcceptCookies(page);

        // Scroll to the bottom of the page
        await ScrollToBottom(page);

        // Retrieve an IEnumerable of the elements holding the data for each monster
        IElementHandle[] monsterDataElements = await ExtractMonsterDataElements(page);
        
        // Go through the list and send all NormalMonsters to the API
        // foreach (IElementHandle monsterDataElement in monsterDataElements)
        //     await AddMonsterToApi(monsterDataElement);
        
        // Go through the list again and send all the drops to the API
        foreach (IElementHandle monsterDataElement in monsterDataElements)
            await AddDropsToApi(monsterDataElement);
        
        // // Update monster with monster relations
        // foreach (IElementHandle monsterDataElement in monsterDataElements)
        //     await UpdateMonstersWithRelatedMonsters(monsterDataElement);
        //
        // // AddMonster Characteristics to the API
        // foreach (IElementHandle monsterDataElement in monsterDataElements)
        //     await AddMonsterCharacteristicsToApi(monsterDataElement);
        //
        // // Go through the list again and send all the Names in french to the API
        // foreach (IElementHandle monsterDataElement in monsterDataElements)
        //     await AddNameToTheApi(monsterDataElement, Language.FR);
        //
        // // Now change the language to english and send all the Names in english to the API
        // await page.GoToAsync(EntryUrlEn);
        // await ScrollToBottom(page);
        //
        // monsterDataElements = await ExtractMonsterDataElements(page);
        // foreach (IElementHandle monsterDataElement in monsterDataElements)
        //     await AddNameToTheApi(monsterDataElement, Language.EN);
        //
        // // Now change the language to spanish and send all the names in spanish to the API
        // await page.GoToAsync(EntryUrlEs);
        // await ScrollToBottom(page);
        //
        // monsterDataElements = await ExtractMonsterDataElements(page);
        // foreach (IElementHandle monsterDataElement in monsterDataElements)
        //     await AddNameToTheApi(monsterDataElement, Language.ES);
        
        // Close browser
        // await _browser.CloseAsync();
    }

    private async Task AcceptCookies(IPage page)
    {
        // Selector for the element you want to click
        string selector = ".fc-cta-consent";

        // Query for the element using the selector and click if found
        IElementHandle? element = await page.QuerySelectorAsync(selector);
        if (element != null)
            await element.ClickAsync();
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
        
        AddLocalizedStringDto addLocalizedStringDto = new AddLocalizedStringDto(
            EntityId: monsterId,
            LanguageId: (int)language,
            Value: name
        );
        
        string jsonToSend = JsonConvert.SerializeObject(addLocalizedStringDto);
        Console.WriteLine(jsonToSend);
        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/monster/name", stringContent);
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
            HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/monster/characteristic", stringContent);
            Console.WriteLine($"{response.StatusCode}");
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
        
        // Get Breed Id
        int breedId = await GetBreedId(anchorElements[1]);
        
        int imageId = await GetImageId(monsterDataElement);

        AddMonsterDto addMonsterDto = new AddMonsterDto(
            monsterId,
            ecosystemId,
            breedId,
            imageId
        );
        
        string jsonToSend = JsonConvert.SerializeObject(addMonsterDto);
        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/monster", stringContent);
        Console.WriteLine($"Post request status code = {response.StatusCode}");
    }

    private async Task AddDropsToApi(IElementHandle mobDataElement)
    {
        string wrapperSelector = "[data-collapse-target='bestiaryCollapseDrops']";
        
        // Check if the monster has a drops compartment!
        IElementHandle? dropWrapperElementHandle = await mobDataElement.QuerySelectorAsync(wrapperSelector);
        if (dropWrapperElementHandle == null)
            return;
        
        // Extract Monster Id
        int monsterId = await GetMonsterId(mobDataElement);
        
        // Extract all the anchor elements that hold the monster Ids
        IElementHandle[] anchorElements = await dropWrapperElementHandle.QuerySelectorAllAsync("a");
        int numberOfDrops = anchorElements.Length;
        
        // Retrieve all item Ids
        int[] itemIds = await ExtractIdsAnchorElements(anchorElements);

        // Extract all the abbr elements
        IElementHandle[] abbrElements = await dropWrapperElementHandle.QuerySelectorAllAsync("abbr");
        // Extract the prospection thresholds
        int[] prospectionThresholds = await ExtractProspectionThresholdsFromAbbrElements(abbrElements, numberOfDrops);

        // Extract drop caps
        int?[] dropCaps = await ExtractDropCapsFromAbbrElements(abbrElements, numberOfDrops);

        // Extract drop chances
        string innerText =
            await dropWrapperElementHandle.EvaluateFunctionAsync<string>("element => element.textContent");
        float?[] dropChances = ExtractDropChancesFromTextContent(innerText, numberOfDrops);

        // Build the AddDropDtos and send them to the API
        for (int i = 0; i < numberOfDrops; i++)
        {
            AddDropDto addDropDto = new AddDropDto(
                MonsterId: monsterId,
                ItemId: itemIds[i],
                Rate: dropChances[i],
                DropCap: dropCaps[i],
                ProspectionThreshold: prospectionThresholds[i]
            );
            string jsonToSend = JsonConvert.SerializeObject(addDropDto);
            StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("http://localhost:5067/api/v1/drop", stringContent);
        }
    }

    private float?[] ExtractDropChancesFromTextContent(string innerText, int numberOfDrops)
    {
        float?[] dropChances = ExtractNumbersBetweenBrackets(innerText, numberOfDrops);
        return dropChances;
    }

    private async Task<int?[]> ExtractDropCapsFromAbbrElements(IElementHandle[] abbrElements, int numberOfDrops)
    {
        int?[] dropCaps = new int?[numberOfDrops];
        int index = 0;
        
        foreach (IElementHandle elementHandle in abbrElements)
        {
            string dropCapTitle = await elementHandle.EvaluateFunctionAsync<string>("element => element.title");
            if (dropCapTitle != "Drops maximum")
                continue;
            
            string dropCapInnerText = await elementHandle.EvaluateFunctionAsync<string>("element => element.innerText");
            if (int.TryParse(dropCapInnerText, NumberStyles.Integer, null, out int prospectionThreshold))
                dropCaps[index] = prospectionThreshold;
            else
                dropCaps[index] = null;
            index++;
        }

        return dropCaps;
    }
    
    private float?[] ExtractNumbersBetweenBrackets(string input, int numberOfDrops)
    {
        // List<int> result = new List<int>();
        float?[] result = new float?[numberOfDrops];

        // Define the regular expression pattern
        string pattern = @"\((\d+(\.\d+)?)% \)";

        // Match the pattern in the input string
        MatchCollection matches = Regex.Matches(input, pattern);
        
        // Extract the floats
        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].Groups.Count == 3 && float.TryParse(matches[i].Groups[1].Value, NumberStyles.Any, CultureInfo.InvariantCulture, out float value))
                result[i] = value;
            else
                result[i] = null;
        }
        return result;
    }

    private async Task<int[]> ExtractProspectionThresholdsFromAbbrElements(IElementHandle[] abbrElements, int numberOfDrops)
    {
        int[] prospectionThresholds = new int[numberOfDrops];
        int index = 0;
        
        foreach (IElementHandle elementHandle in abbrElements)
        {
            string prospectionThresholdTitle = await elementHandle.EvaluateFunctionAsync<string>("element => element.title");
            if (prospectionThresholdTitle == "Drops maximum")
                continue;
            prospectionThresholdTitle = prospectionThresholdTitle.Replace("PP", "");
            if (int.TryParse(prospectionThresholdTitle, NumberStyles.Integer, null, out int prospectionThreshold))
                prospectionThresholds[index] = prospectionThreshold;
            else
                prospectionThresholds[index] = 0;
            index++;
        }
        return prospectionThresholds;
    }

    private async Task<int[]> ExtractIdsAnchorElements(IElementHandle[] anchorElements)
    {
        int[] ids = new int[anchorElements.Length];
        
        for (int i = 0; i < anchorElements.Length; i++)
        {
            ids[i] = await ExtractIdFromAnchorElement(anchorElements[i]);
        }

        return ids;
    }

    private async Task<string[]> ExtractHrefsFromAnchorElements(IElementHandle[] anchorElements)
    {
        string[] hrefs = new string[anchorElements.Length];
        
        for (int i = 0; i < anchorElements.Length; i++)
        {
            hrefs[i] = await anchorElements[i].EvaluateFunctionAsync<string>("element => element.href");
        }

        return hrefs;
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
        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync("http://localhost:5067/api/v1/monster", stringContent);
        Console.WriteLine($"Put request status code = {response.StatusCode}");
    }

    private async Task<int> GetEcosystemId(IElementHandle anchorElement)
    {
        return await ExtractIdFromAnchorElement(anchorElement);
    }

    private async Task<int> GetImageId(IElementHandle monsterDataElement)
    {
        IElementHandle imgWrapperElement = await monsterDataElement.QuerySelectorAsync("div.card-solo-monster-img");
        
        IElementHandle imgElement = await imgWrapperElement.QuerySelectorAsync("img");
        
        return await ExtractIdFromImgElement(imgElement);
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

        string? stringId = ExtractFirstNumericValueFromString(href); 

        if (stringId == null)
            Console.WriteLine("Something went wrong extracting Id from Anchor Element...");
        else if (int.TryParse(stringId, out int id))
            return id;
        return -1;
    }

    private async Task<int> ExtractIdFromImgElement(IElementHandle imgElement)
    {
        string src = await imgElement.EvaluateFunctionAsync<string>("element => element.src");
        
        string? stringId = ExtractFirstNumericValueFromString(src);
        
        if (stringId == null)
            Console.WriteLine("Something went wrong extracting Id from Img Element...");
        else if (int.TryParse(stringId, out int id))
            return id;
        return -1;
    }

    private string? ExtractFirstNumericValueFromString(string str)
    {
        // Regular expression to extract the first numeric value from the string
        Regex regex = new Regex(@"(\d+)");

        // Match the regular expression against the input string
        Match match = regex.Match(str);

        if (match.Success && match.Groups.Count > 1)
        {
            // The numeric ID is in the first group
            return match.Groups[1].Value;
        }
        return null;
    }
    
    private async Task<IElementHandle[]> ExtractMonsterDataElements(IPage page)
    {
        // Selector for the elements you want to extract
        string selector = ".container-fluid.mt-4[data-mob='']";

        // Query for the elements using the selector
        IElementHandle[] elements = await page.QuerySelectorAllAsync(selector);
        return elements;
    }
    
    private async Task<string> ExtractMonsterName(IElementHandle mobDataElement)
    {
        // Selector for the element to extract
        string selector = ".text-sololightbeige";

        // Query for the element using the selector
        IElementHandle element = await mobDataElement.QuerySelectorAsync(selector);
        return await element.EvaluateFunctionAsync<string>("element => element.innerText");
    }
}