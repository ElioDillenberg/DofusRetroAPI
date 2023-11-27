using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Items.ItemEffectDto;
using ClassLibrary.Enums.ItemEffects;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class ItemStatsActionScriptParser : ActionScriptParserBase
{
    public override async Task ParseDecompiledFiles()
    {
        string[] itemSourceDirectories = GetItemSourceDirectories("itemstats_*");

        await AddItemStatsToApi(itemSourceDirectories);
    }

    private async Task AddItemStatsToApi(string[] itemSourceDirectories)
    {
        // Retrieve the ItemStatsJsons from the french source file
        Dictionary<int, string>? rawItemStatsData = null;
        string? frenchDirectory = itemSourceDirectories.FirstOrDefault(x => x.Contains("_fr"));
        if (frenchDirectory != null)
            rawItemStatsData = ParseItemStatsFiles(frenchDirectory);
        else
            return;

        foreach (KeyValuePair<int, string> rawItemStat in rawItemStatsData)
        {
            int itemId = rawItemStat.Key;
            string[] itemStats = CleanItemStatString(rawItemStat.Value);
            foreach (string itemStat in itemStats)
            {
                // Build the AddItemEffectDto
                AddItemEffectDto? addItemEffectDto = ExtractItemStatDto(itemId, itemStat);
                if (addItemEffectDto == null)
                    continue;

                string jsonToSend = JsonConvert.SerializeObject(addItemEffectDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response =  await _httpClient.PostAsync("http://localhost:5067/api/v1/item/effect", stringContent);
            }
        }
    }

    private Dictionary<int, string> ParseItemStatsFiles(string folderPath)
    {
        Dictionary<int, string> result = new Dictionary<int, string>();
        string[] files = Directory.GetFiles(folderPath, "*.as");
        foreach (string file in files)
            ParseFileForItemData(file);
        return result;

        void ParseFileForItemData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (line.StartsWith("ISTA["))
                {
                    // Extract the integer between square braces
                    Match match = Regex.Match(line, @"ISTA\[(\d+)\]");
                    if (match.Success)
                    {
                        int key = int.Parse(match.Groups[1].Value);
                        // Extract and parse the string starting from "ISTA["
                        int startIndex = line.IndexOf('"');
                        int endIndex = line.LastIndexOf('"');
                        if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                        {
                            string statsString = line.Substring(startIndex, endIndex - startIndex + 1);
                            result[key] = statsString;
                        }
                    }
                }
            }
        }
    }
    
    private AddItemEffectDto? ExtractItemStatDto(int itemId, string itemStat)
    {
        AddItemEffectDto? addItemEffectDto = null;
        
        // Protecting from empty strings
        if (itemStat == "")
        {
            Console.WriteLine($"ItemId[{itemId}] -> itemStat is empty");
            return null;
        }

        // Separate the itemStat string into its 4 components
        string[] substrings = SplitItemStatString(itemStat);
        
        // Convert hexadecimal ItemEffectType string component to int (decimal)
        int effectTypeInt;
        if (!int.TryParse(substrings[0], System.Globalization.NumberStyles.HexNumber, null, out effectTypeInt))
        {
            Console.WriteLine($"ItemId[{itemId}] -> ItemEffectType {substrings[0]} could not be parsed to a decimal int from a hexadecimal string.");
            return null;
        }
            
        // Now we check that we have a defined ItemEffectType enum for this value, otherwise skip this stat all together by returning null
        ItemEffectType effectType;
        if (Enum.IsDefined(typeof(ItemEffectType), effectTypeInt))
            effectType = (ItemEffectType)effectTypeInt;
        else
        {
            Console.WriteLine($"ItemId[{itemId}] -> ItemEffectType {effectTypeInt} is not defined in the ItemEffectType enum.");
            return null;
        }
        
        // Convert hexadecimal StatMinValue string component to int (decimal)
        int effectMinValue; 
        if (substrings[1] == "null" || substrings[1] == "")
            effectMinValue = 0;
        else if (!int.TryParse(substrings[1], System.Globalization.NumberStyles.HexNumber, null, out effectMinValue))
            effectMinValue = -1;
        
        // Convert hexadecimal StatMaxValue string component to int (decimal)
        int effectMaxValue;
        if (substrings[2] == "null"  || substrings[2] == "")
            effectMaxValue = effectMinValue;
        else if (!int.TryParse(substrings[2], System.Globalization.NumberStyles.HexNumber, null, out effectMaxValue))
            effectMaxValue = -1;
        
        // Create the AddItemEffectDto and return
        addItemEffectDto = new AddItemEffectDto(
            ItemId: itemId,
            EffectType: (int)effectType,
            MinValue: effectMinValue,
            MaxValue: effectMaxValue
        );
        return addItemEffectDto;
    }
    
    private string[] SplitItemStatString(string itemStat)
    {
        // Format the string for easier splitting
        string itemStatWithNull = itemStat.Replace("###", "#null#");
        itemStatWithNull = itemStatWithNull.Replace("##", "#");
        
        // Split the input string based on '#'
        string[] substrings = itemStatWithNull.Split('#');

        // Process the substrings to include '#' as separate substrings
        StringBuilder resultBuilder = new StringBuilder();
        foreach (string substring in substrings)
        { 
            if (!string.IsNullOrEmpty(substring))
                resultBuilder.Append(substring).Append('#');
        }

        // Return the processed substrings
        return resultBuilder.ToString().Split('#');
    }

    private string[] CleanItemStatString(string itemStats)
    {
        string strippedRawItemStat = itemStats.Replace("\"", "");
        string[] cleanItemStats = strippedRawItemStat.Split(',');
        return cleanItemStats;
    }
}
    
    