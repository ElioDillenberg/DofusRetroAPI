// See https://aka.ms/new-console-template for more information

using ScraperDofusRetroAPI.Scrapers;

// IScraper monsterScraper = new MonsterScraper(false);
// await monsterScraper.Scrape();

IScraper itemScraper = new ItemScraper(false);
await itemScraper.Scrape();