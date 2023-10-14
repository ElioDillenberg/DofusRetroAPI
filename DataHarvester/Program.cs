// See https://aka.ms/new-console-template for more information

using ClassLibrary.DTOs.Items.ItemConditionDto;
using DataHarvester;
using DataHarvester.LangDecompilers.ActionScriptParsers;
using DataHarvester.LangDecompilers.SwfDecompiling;

using DataHarvester.Scrapers;
using Newtonsoft.Json;

// IScraper monsterScraper = new MonsterScraper(false);
// await monsterScraper.Scrape();

// IScraper itemScraper = new ItemScraper(false);
// await itemScraper.Scrape();

// SwfDecompiler swfDecompiler = new SwfDecompiler(); 
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemStats, Language.FR);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemStats, Language.EN);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemStats, Language.ES);
//
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemSets, Language.FR);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemSets, Language.EN);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemSets, Language.ES);

ItemActionScriptParser itemActionScriptParser = new ItemActionScriptParser();
await itemActionScriptParser.ParseDecompiledFiles();

// SetActionScriptParser setActionScriptParser = new SetActionScriptParser();
// await setActionScriptParser.ParseDecompiledFiles();

// SandBox.ParseConditionString("Sc=15&(PB=2|PB=96|PB=480)&(Sc!1003|PB!2)&(Sc!1006|PB!96)", 1);
// SandBox.ParseConditionString("Sc=15&(PB=2|PB=96|PB=480)&(Sc!1003|PB!2)&(Sc!1006|PB!96)", 1);
// AddItemConditionDto[] addItemConditionDtos  = SandBox.ParseConditionString("CA>92&CS>19", 1);

// string jsonToSend = JsonConvert.SerializeObject(addItemConditionDtos);
// string prettyJson = JsonConvert.SerializeObject(addItemConditionDtos, Formatting.Indented);

// Console.WriteLine(prettyJson);