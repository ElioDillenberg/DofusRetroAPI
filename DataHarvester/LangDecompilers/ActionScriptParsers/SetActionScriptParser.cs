using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Sets.SetDto;
using DataHarvester.LangDecompilers.JsonStructures;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class SetActionScriptParser : ActionScriptParserBase
{
    public override async Task ParseDecompiledFiles()
    {
        // Get all directories in the decompiled files directory that start with "items_"
        string[] itemSourceDirectories = Directory.GetDirectories($"{_decompiledFilesDirectoryBasePath}", "itemsets_*");
        // Add the frame_1 folder to the path
        for (int i = 0; i < itemSourceDirectories.Length; i++)
            itemSourceDirectories[i] = String.Concat(itemSourceDirectories[i], "\\frame_1");
        
        // To retrieve the use the french source files
        Dictionary<int, SetJson>? rawSetData = null;
        string? frenchDirectory = itemSourceDirectories.FirstOrDefault(x => x.Contains("_fr"));
        if (frenchDirectory != null)
            rawSetData = ParseSetFiles(frenchDirectory);
        
        if (rawSetData != null)
            foreach (KeyValuePair<int, SetJson> set in rawSetData)
            {
                // Add Set to API
                AddSetDto addSetDto = new AddSetDto(
                    set.Key,
                    set.Value.EquipmentIds
                );
                
                string jsonToSend = JsonConvert.SerializeObject(addSetDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/Set", stringContent);
            }
    }

    private Dictionary<int, SetJson> ParseSetFiles(string folderPath)
    {
        Dictionary<int, SetJson> result = new Dictionary<int, SetJson>();
        string[] files = Directory.GetFiles(folderPath, "*.as");
        foreach (string file in files)
            ParseFileForSetData(file);
        return result;

        void ParseFileForSetData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("IS["))
                {
                    // Extract the integer between square braces
                    Match match = Regex.Match(line, @"IS\[(\d+)\]");
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
                            SetJson? setJson = JsonConvert.DeserializeObject<SetJson>(processedString);
                            if (setJson != null)
                                result[key] = setJson;
                        }
                    }
                }
            }
        }
    }
            
    private string ProcessString(string input)
    {
        string result = input.Replace("\\", "");
        return result;
    }
}