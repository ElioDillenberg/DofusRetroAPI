using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.Enums.Languages;
using DataHarvester.LangDecompilers.JsonStructures;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

/// <summary>
/// This class is responsible for parsing the ActionScript files that contain the effects of the items and sending the localized names in all languages
/// </summary>
public class EffectsActionScriptParser : ActionScriptParserBase
{
    public override async Task ParseDecompiledFiles()
    {
        string[] effectsSourceDirectories = GetItemSourceDirectories("effects_*");

        // Add the localized names and descriptions to the API
        await AddLocalizedEffectsNameToApi(effectsSourceDirectories, Language.FR, "_fr");
        await AddLocalizedEffectsNameToApi(effectsSourceDirectories, Language.EN, "_en");
        await AddLocalizedEffectsNameToApi(effectsSourceDirectories, Language.ES, "_es");
    }

    private async Task AddLocalizedEffectsNameToApi(
        string[] effectsSourceDirectories,
        Language language,
        string languageFileExtension)
    {
        Dictionary<int, string>? rawEffectsData = null;
        string? languageDirectory = effectsSourceDirectories.FirstOrDefault(x => x.Contains(languageFileExtension));
        if (languageDirectory != null)
            rawEffectsData = ParseEffectsFiles(languageDirectory);
        
        // Now map all the effects to 
        if (rawEffectsData != null)
        {
            foreach (KeyValuePair<int, string> item in rawEffectsData)
            {
                // Add the localized name to the API
                AddLocalizedStringDto addLocalizedStringDto = new AddLocalizedStringDto
                (
                    EntityId: item.Key,
                    LanguageId: (int)language,
                    Value: item.Value
                );

                string jsonToSend = JsonConvert.SerializeObject(addLocalizedStringDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await _httpClient.PostAsync("http://localhost:5067/api/v1/item/effect/text", stringContent);
                Console.WriteLine(response.StatusCode);
            }
        }
    }
    
    private Dictionary<int, string> ParseEffectsFiles(string folderPath)
    {
        Dictionary<int, string> result = new Dictionary<int, string>();
        string[] filePaths = Directory.GetFiles(folderPath, "*.as");
        foreach (string filePath in filePaths)
            ParseFileForEffectsString(filePath);
        return result;

        void ParseFileForEffectsString(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (line.Contains("E["))
                {
                    // Extract the integer between square braces
                    Match match = Regex.Match(line, @"E\[(\d+)\]");
                    if (match.Success)
                    {
                        int key = int.Parse(match.Groups[1].Value);
                        // Extract and parse the JSON object starting from "{" and ending at "}"
                        int startIndex = line.IndexOf('{');
                        int endIndex = line.LastIndexOf('}');
                        if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                        {
                            string jsonString = line.Substring(startIndex, endIndex - startIndex + 1);
                            string processedString = ProcessString(jsonString);
                            EffectJson? effectJson = JsonConvert.DeserializeObject<EffectJson>(processedString);
                            if (effectJson != null)
                                result[key] = effectJson.Text;
                        }
                    }
                }
            }
        }
    }
    
    private string ProcessString(string input)
    {
        string result = input.Replace("\" + \"", "");
        return result;
    }
    

}