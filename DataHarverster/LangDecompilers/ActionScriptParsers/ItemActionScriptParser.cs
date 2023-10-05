using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Items.ItemDto;
using DataHarvester.LangDecompilers.JsonStructures;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class ItemActionScriptParser : ActionScriptParserBase
{
    public ItemActionScriptParser() : base() { }

    public override async Task ParseDecompiledFiles()
    {
        // Get all directories in the decompiled files directory that start with "items_"
        string[] itemSourceDirectories = Directory.GetDirectories($"{_decompiledFilesDirectoryBasePath}", "items_*");
        // Add the frame_1 folder to the path
        for (int i = 0; i < itemSourceDirectories.Length; i++)
            itemSourceDirectories[i] = String.Concat(itemSourceDirectories[i], "\\frame_1");
        
        // To retrieve the items, can work with any of the decompiled directories as there is no localization involved
        Dictionary<int, ItemJson>? rawItemData = null;
        string? frenchDirectory = itemSourceDirectories.FirstOrDefault(x => x.Contains("_fr"));
        if (frenchDirectory != null)
            rawItemData = ParseItemFiles(frenchDirectory);
        
        // Now map all the ItemJson objects to AddItemDto objects to send them to the API
        if (rawItemData != null)
            foreach (KeyValuePair<int, ItemJson> item in rawItemData)
            {
                // Add Item
                AddItemDto dto = new AddItemDto(
                    item.Key,
                    item.Value.Level,
                    item.Value.ItemType,
                    item.Value.Pods,
                    item.Value.GfxId
                );
                
                string jsonToSend = JsonConvert.SerializeObject(dto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("http://localhost:5067/api/v1/Item", stringContent);
                
                // Add Item Names (french)
                
                // Add Item Weapon Caracteristics
                
                // Add Item Conditions
                
                // Add Item Recipes
            }
    }

    private Dictionary<int, ItemJson> ParseItemFiles(string folderPath)
    {
        Dictionary<int, ItemJson> result = new Dictionary<int, ItemJson>();
        string[] files = Directory.GetFiles(folderPath, "*.as");
        foreach (string file in files)
            ParseFileForItemData(file);    
        return result;
        
        void ParseFileForItemData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("I.u["))
                {
                    // Extract the integer between square braces
                    Match match = Regex.Match(line, @"I\.u\[(\d+)\]");
                    if (match.Success)
                    {
                        int key = int.Parse(match.Groups[1].Value);
                        // Extract and parse the JSON object starting from "I.u["
                        int startIndex = line.IndexOf('{');
                        int endIndex = line.LastIndexOf('}');
                        if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                        {
                            string jsonString = line.Substring(startIndex, endIndex - startIndex + 1);
                            string processedString = ProcessString(jsonString);
                            ItemJson? itemJson = JsonConvert.DeserializeObject<ItemJson>(processedString);
                            if (itemJson != null)
                                result[key] = itemJson;
                        }
                    }
                }
            }
        }
    }
    
    public string ProcessString(string input)
    {
        string result = input.Replace("\" + \"", "");
        return result;
    }
}