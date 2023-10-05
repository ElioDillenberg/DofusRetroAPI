
namespace DataHarvester.Scrapers;

// public class ItemScraper : BaseScraper
// {
//     public ItemScraper(bool headless) : base(headless) { }
//     
//     private const string EntryUrlFr = "https://solomonk.fr/fr/monstres/chercher";
//     
//     public override async Task Scrape()
//     {
//         // Instead of scraping, we could just look at the lang and figure out if we have all the data we need from there?
//         
//         // Open new page
//         // IPage page = await _browser.NewPageAsync();
//         
//         // Go to entry url
//         // await page.GoToAsync(EntryUrlFr);
//
//         // Scroll to the bottom of the page
//         // await ScrollDownABit(page);
//         // await ScrollToBottom(page);
//         
//         // Issue here is that we have several categories of items to scrape...
//     }
// }