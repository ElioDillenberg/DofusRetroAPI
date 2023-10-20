using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Items.ItemEffectDto;
using ClassLibrary.Enums.Stats;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class ItemStatsActionScriptParser : ActionScriptParserBase
{
    public override async Task ParseDecompiledFiles()
    {
        string[] itemSourceDirectories =
            Directory.GetDirectories($"{_decompiledFilesDirectoryBasePath}", "itemstats_*");

        // Add the frame_1 folder to the path
        for (int i = 0; i < itemSourceDirectories.Length; i++)
            itemSourceDirectories[i] = String.Concat(itemSourceDirectories[i], "\\frame_1");

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
            if (rawItemStat.Key > 50)
                return;
            int itemId = rawItemStat.Key;
            string[] itemStats = CleanItemStatString(rawItemStat.Value);
            foreach (string itemStat in itemStats)
            {
                Console.WriteLine($"ItemId = {itemId}, ItemStat = {itemStat}");
                // Build the AddItemStatDto
                AddItemStatDto? addItemStatDto = ExtractItemStatDto(itemId, itemStat);
                if (addItemStatDto == null)
                    continue;

                string jsonToSend = JsonConvert.SerializeObject(addItemStatDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("http://localhost:5067/api/v1/item/stat", stringContent);    
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
    
    private AddItemStatDto? ExtractItemStatDto(int itemId, string itemStat)
    {
        AddItemStatDto? addItemStatDto = null;

        string[] substrings = SplitItemStatString(itemStat);
        
        // Convert hexadecimal StatType string component to int (decimal)
        int statTypeInt = int.Parse(substrings[0], System.Globalization.NumberStyles.HexNumber);
        // Now we check that we have a defined StatType enum for this value, otherwise skip this stat all together by returning null
        StatType statType;
        if (Enum.IsDefined(typeof(StatType), statTypeInt))
            statType = (StatType)statTypeInt;
        else 
            return null;
        
        // Convert hexadecimal StatMinValue string component to int (decimal)
        int statMinValue = int.Parse(substrings[1], System.Globalization.NumberStyles.HexNumber);
        
        // Convert hexadecimal StatMaxValue string component to int (decimal)
        int statMaxValue;
        if (substrings[2] == "null")
            statMaxValue = statMinValue;
        else 
            statMaxValue = int.Parse(substrings[2], System.Globalization.NumberStyles.HexNumber);
        
        addItemStatDto = new AddItemStatDto(
            ItemId: itemId,
            StatType: (int)statType,
            StatMinValue: statMinValue,
            StatMaxValue: statMaxValue
        );

        return addItemStatDto;
    }
    
    private string[] SplitItemStatString(string itemStat)
    {
        // Format the string for easier splitting
        string itemStatWithNull = itemStat.Replace("###", "#null#");
        itemStatWithNull = itemStatWithNull.Replace("##", "#");
        
        // Split the input string based on '#'
        string[] substrings = itemStatWithNull.Split('#');
        foreach (string str in substrings)
            Console.WriteLine(str);

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
    
    