// See https://aka.ms/new-console-template for more information

using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.Enums.Languages;
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
// swfDecompiler.DecompileFiles(SwfSourceFileType.ItemEffects, Language.FR);
// swfDecompiler.DecompileFiles(SwfSourceFileType.ItemEffects, Language.EN);
// swfDecompiler.DecompileFiles(SwfSourceFileType.ItemEffects, Language.ES);

// swfDecompiler.DecompileItems(SwfSourceFileType.ItemsStats, Language.FR);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemStats, Language.EN);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemStats, Language.ES);

// swfDecompiler.DecompileItems(SwfSourceFileType.ItemSets, Language.FR);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemSets, Language.EN);
// swfDecompiler.DecompileItems(SwfSourceFileType.ItemSets, Language.ES);

// IActionScriptParser itemActionScriptParser = new ItemActionScriptParser();
// await itemActionScriptParser.ParseDecompiledFiles();

// IActionScriptParser setActionScriptParser = new SetActionScriptParser();
// await setActionScriptParser.ParseDecompiledFiles();

// IActionScriptParser itemStatsActionScriptParser = new ItemStatsActionScriptParser();
// await itemStatsActionScriptParser.ParseDecompiledFiles();

IActionScriptParser itemEffectsActionScriptParser = new EffectsActionScriptParser();
await itemEffectsActionScriptParser.ParseDecompiledFiles();

// SandBox.ParseConditionString("Sc=15&(PB=2|PB=96|PB=480)&(Sc!1003|PB!2)&(Sc!1006|PB!96)", 1);
// SandBox.ParseConditionString("Sc=15&(PB=2|PB=96|PB=480)&(Sc!1003|PB!2)&(Sc!1006|PB!96)", 1);
// AddItemConditionDto[] addItemConditionDtos  = SandBox.ParseConditionString("CA>92&CS>19", 1);

// string jsonToSend = JsonConvert.SerializeObject(addItemConditionDtos);
// string prettyJson = JsonConvert.SerializeObject(addItemConditionDtos, Formatting.Indented);

// Console.WriteLine(prettyJson);