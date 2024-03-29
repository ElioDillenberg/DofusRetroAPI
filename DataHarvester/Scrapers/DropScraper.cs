using PuppeteerSharp;

namespace DataHarvester.Scrapers;

public class DropScraper : BaseScraper
{
    private const string EntryUrlFr = "https://solomonk.fr/fr/monstres/chercher";
    
    public DropScraper(bool headless) : base(headless) { }

    public override async Task Scrape()
    {
        // Open new page
        IPage page = await _browser.NewPageAsync();
        
        // Go to entry url
        await page.GoToAsync(EntryUrlFr);

        // Scroll to the bottom of the page
        await ScrollToBottom(page);
        
        // Retrieve an IEnumerable of the elements holding the data for each monster
        IElementHandle[] monsterDataElements = await ExtractMonsterDataElements(page);

        // foreach (IElementHandle monsterDataElement in monsterDataElements)
        //     await AddDropsToApi(monsterDataElement);
    }
    
    private async Task<IElementHandle[]> ExtractMonsterDataElements(IPage page)
    {
        // Selector for the elements you want to extract
        string selector = ".container-fluid.mt-4[data-mob='']";

        // Query for the elements using the selector
        IElementHandle[] elements = await page.QuerySelectorAllAsync(selector);
        return elements;
    }
}