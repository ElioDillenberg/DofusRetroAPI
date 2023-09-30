using System.Text;
using System.Text.RegularExpressions;
using DofusRetroClassLibrary.DTOs.Monsters.NormalMonster;
using Newtonsoft.Json;

namespace ScraperDofusRetroAPI.Scrapers;
using PuppeteerSharp;

public class MonsterScraper
{
    private IBrowser _browser = null!;
    private const string EntryUrl = "https://solomonk.fr/fr/monstres/chercher";

    private HttpClient _httpClient = null!;

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
    
    public async Task ScrapeNormalMonsters()
    {
        // Open new page
        IPage page = await _browser.NewPageAsync();
        
        // Go to entry url
        await page.GoToAsync(EntryUrl);

        // Scroll to the bottom of the page
        // await ScrollToBottom(page);
        await ScrollDownABit(page);

        // Retrieve an IEnumerable of the elements holding the data for each monster
        IEnumerable<IElementHandle> mobDataElements = await ExtractMobDataElements(page);
        
        // Go through the list and retrieve MonsterId, BreedId and EcosystemId 
        foreach (IElementHandle mobDataElement in mobDataElements)
        {
            await AddMonsterToApi(mobDataElement);
        }
        
        // Close
        await _browser.CloseAsync();
    }

    private async Task AddMonsterToApi(IElementHandle mobDataElement)
    {
        // Get Monster Id
        int monsterId = await GetMonsterId(mobDataElement);
        if (monsterId == -1)
            Console.WriteLine("Something went wrong getting a monster Id...");
            
        // Get wrapper element for Ecosystem and Breed
        IElementHandle smallElement = await mobDataElement.QuerySelectorAsync("small.col.pl-2");
        
        // AnchorElements holding the Ecosystem and Breed
        IElementHandle[] anchorElements = await smallElement.QuerySelectorAllAsync("a");
        
        foreach (IElementHandle anchorElement in anchorElements)
        {
            string innerText = await anchorElement.EvaluateFunctionAsync<string>("element => element.innerText");
            Console.WriteLine(innerText);
        }

        // Get Ecosystem Id
        int ecosystemId = await GetEcosystemId(anchorElements[0]);
        if (ecosystemId == -1)
            Console.WriteLine("Something went wrong getting an ecosystem Id...");
        
        // Get Breed Id
        int breedId = await GetBreedId(anchorElements[1]);
        if (breedId == -1)
            Console.WriteLine("Something went wrong getting a breed Id...");

        if (breedId == 78 && ecosystemId == 20)
        {
            Console.WriteLine("ArchMonster - no handled for now");
            return;
        }
        
        AddNormalMonsterDto addNormalMonsterDto = new AddNormalMonsterDto(
            monsterId,
            ecosystemId,
            breedId
        );
        
        // Console.WriteLine($"AddNormalMonsterDto:\n" +
        //                   $"MonsterId = {addNormalMonsterDto.Id}\n" +
        //                   $"EcosystemId = {addNormalMonsterDto.Ecosystem}\n" +
        //                   $"BreedId = {addNormalMonsterDto.Breed}\n");
        
        string jsonToSend = JsonConvert.SerializeObject(addNormalMonsterDto);
        Console.WriteLine(jsonToSend);
        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/Monster/NormalMonster?language=1", stringContent);
        Console.WriteLine($"Post request status code = {response.StatusCode}");
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
        int previousHeight = -1;
        int currentHeight = await page.EvaluateExpressionAsync<int>("document.body.scrollHeight");

        while (currentHeight > previousHeight)
        {
            // Scroll down using the mouse wheel
            await page.Mouse.WheelAsync(0, 2000);

            // Wait for a short duration to let the content load
            await page.WaitForTimeoutAsync(500);

            // Update heights
            previousHeight = currentHeight;
            currentHeight = await page.EvaluateExpressionAsync<int>("document.body.scrollHeight");
        }
    }
    
    private async Task<IEnumerable<IElementHandle>> ExtractMobDataElements(IPage page)
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