// See https://aka.ms/new-console-template for more information

using DataHarvester.LangDecompilers.ActionScriptParsers;

// using DataHarvester.Scrapers;

// IScraper monsterScraper = new MonsterScraper(false);
// await monsterScraper.Scrape();

// IScraper itemScraper = new ItemScraper(false);
// await itemScraper.Scrape();

// SwfDecompiler swfDecompiler = new SwfDecompiler();
// swfDecompiler.DecompileItems(SwfSourceFileType.Items, Language.FR);
// swfDecompiler.DecompileItems(SwfSourceFileType.Items, Language.EN);
// swfDecompiler.DecompileItems(SwfSourceFileType.Items, Language.ES);

ItemActionScriptParser itemActionScriptParser = new ItemActionScriptParser();
await itemActionScriptParser.ParseDecompiledFiles();