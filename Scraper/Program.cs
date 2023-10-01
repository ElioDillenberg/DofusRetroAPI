// See https://aka.ms/new-console-template for more information

using ScraperDofusRetroAPI.Scrapers;

MonsterScraper monsterScraper = new MonsterScraper(false);

await monsterScraper.ScrapeMonsters();
