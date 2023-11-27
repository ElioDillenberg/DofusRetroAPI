using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Sets.SetDto;
using ClassLibrary.Enums.Languages;
using DataHarvester.LangDecompilers.JsonStructures;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class SetActionScriptParser : ActionScriptParserBase
{
    public override async Task ParseDecompiledFiles()
    {
        string[] itemSourceDirectories = GetItemSourceDirectories("itemsets_*");
        
        await AddSetsToApi(itemSourceDirectories);
        
        await AddSetNamestoApi(itemSourceDirectories, Language.FR, "_fr");
        await AddSetNamestoApi(itemSourceDirectories, Language.EN, "_en");
        await AddSetNamestoApi(itemSourceDirectories, Language.ES, "_es");
    }
    
    private async Task AddSetsToApi(string[] itemSourceDirectories)
    {
        // To retrieve the SetJson from the french source files
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
    
    private async Task AddSetNamestoApi(
        string[] itemSourceDirectories,
        Language language,
        string languageFileExtension)
    {
        // Retrieve the SetJson from the target language source files
        Dictionary<int, SetJson>? rawSetData = null;
        string? directory = itemSourceDirectories.FirstOrDefault(x => x.Contains(languageFileExtension));
        if (directory != null)
            rawSetData = ParseSetFiles(directory);
        if (rawSetData != null)
        {
            foreach (KeyValuePair<int, SetJson> set in rawSetData)
            {
                // Add SetName
                AddLocalizedStringDto addLocalizedStringDto = new AddLocalizedStringDto(
                    EntityId: set.Key,
                    LanguageId: (int)language,
                    Value: set.Value.Name
                );
                string jsonToSend = JsonConvert.SerializeObject(addLocalizedStringDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await _httpClient.PostAsync("http://localhost:5067/api/v1/set/name", stringContent);
                Console.WriteLine(response.StatusCode);
            }
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