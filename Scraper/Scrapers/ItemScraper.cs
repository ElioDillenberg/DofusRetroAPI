using PuppeteerSharp;

namespace ScraperDofusRetroAPI.Scrapers;

public class ItemScraper : BaseScraper
{
    public ItemScraper(bool headless) : base(headless) { }
    
    private const string EntryUrlFr = "https://solomonk.fr/fr/monstres/chercher";

    private Dictionary<int, string> _weaponUrls = new()
    {
        
    }; 

    private Dictionary<int, string> _equipmentUrls = new()
    {
        
    };
    
    private Dictionary<int, string> _consumableUrls = new()
    {
        
    };
    
    private Dictionary<int, string> _resourceUrls = new()
    {
        
    };
    
    public override async Task Scrape()
    {
        // Open new page
        IPage page = await _browser.NewPageAsync();
        
        // Go to entry url
        await page.GoToAsync(EntryUrlFr);

        // Scroll to the bottom of the page
        await ScrollDownABit(page);
        // await ScrollToBottom(page);
        
        
        
        // Issue here is that we have several categories of items to scrape...
        
        
        
    }
}