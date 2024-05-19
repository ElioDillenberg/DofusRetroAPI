using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.DTOs.Monsters.MonsterCharacteristicDto;
using ClassLibrary.Enums.Languages;
using DataHarvester.LangDecompilers.JsonStructures;
using DofusRetroClassLibrary.DTOs.Monsters.MonsterDto;
using Newtonsoft.Json;
using PuppeteerSharp;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class MonstersActionScriptParser : ActionScriptParserBase
{
    public async override Task ParseDecompiledFiles()
    {
        // Get all directories in the decompiled files directory that start with "items_"
        string[] monstersSourceDirectories = GetItemSourceDirectories("items_*");

        // Add the Items to the API
        await AddMonstersToApi(monstersSourceDirectories);

        // Add the localized names and descriptions to the API
        await AddLocalizedNamesToApi(monstersSourceDirectories, Language.FR, "_fr");
        await AddLocalizedNamesToApi(monstersSourceDirectories, Language.EN, "_en");
        await AddLocalizedNamesToApi(monstersSourceDirectories, Language.ES, "_es");
    }
    
    
    /// <summary>
    /// Names
    /// </summary>
    /// <param name="monstersSourceDirectories"></param>
    /// <param name="languageFileExtension"></param>
    private async Task AddMonsterStatsToApi(
        string[] monstersSourceDirectories,
        string languageFileExtension)
    {
        // Retrieve the MonsterJson from the target language source files
        Dictionary<int, MonsterJson>? rawMonsterData = null;
        string? directory = monstersSourceDirectories.FirstOrDefault(x => x.Contains(languageFileExtension));
        if (directory != null)
            rawMonsterData = ParseMonsterFiles(directory);
        if (rawMonsterData != null)
        {
            foreach (KeyValuePair<int, MonsterJson> monster in rawMonsterData)
            {
                foreach (MonsterCharacteristics? monsterCharacteristics in monster.Value.Characteristics)
                {
                    if (monsterCharacteristics != null)
                    {
                        // Add SetName
                        AddMonsterCharacteristicDto addMonsterCharacteristicDto = new (
                            Rank: monsterCharacteristics.Rank,
                            MonsterId: monster.Key,
                            Level: monsterCharacteristics.Level,
                            ActionPoints: monsterCharacteristics.ActionPoints,
                            MovementPoints: monsterCharacteristics.MovementPoints,
                            NeutralResistancePercentage: monsterCharacteristics.Resistances[0],
                            EarthResistancePercentage: monsterCharacteristics.Resistances[1],
                            FireResistancePercentage: monsterCharacteristics.Resistances[2],
                            WaterResistancePercentage: monsterCharacteristics.Resistances[3],
                            AirResistancePercentage: monsterCharacteristics.Resistances[4],
                            ActionPointAvoidance: monsterCharacteristics.Resistances[5],
                            MovementPointAvoidance: monsterCharacteristics.Resistances[6],
                            HealthPoints: null,
                            Experience: null,
                            Initiative: null,
                            Strength: null,
                            Intelligence: null,
                            Chance: null,
                            Agility: null
                        );
                        string jsonToSend = JsonConvert.SerializeObject(addMonsterCharacteristicDto);
                        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                        HttpResponseMessage response =
                            await _httpClient.PostAsync("http://localhost:5067/api/v1/monster/characteristic", stringContent);
                        Console.WriteLine(response.StatusCode);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Names
    /// </summary>
    /// <param name="monstersSourceDirectories"></param>
    /// <param name="language"></param>
    /// <param name="languageFileExtension"></param>
    private async Task AddLocalizedNamesToApi(
        string[] monstersSourceDirectories,
        Language language,
        string languageFileExtension)
    {
        // Retrieve the MonsterJson from the target language source files
        Dictionary<int, MonsterJson>? rawMonsterData = null;
        string? directory = monstersSourceDirectories.FirstOrDefault(x => x.Contains(languageFileExtension));
        if (directory != null)
            rawMonsterData = ParseMonsterFiles(directory);
        if (rawMonsterData != null)
        {
            foreach (KeyValuePair<int, MonsterJson> monster in rawMonsterData)
            {
                // Add SetName
                AddLocalizedStringDto addLocalizedStringDto = new AddLocalizedStringDto(
                    EntityId: monster.Key,
                    LanguageId: (int)language,
                    Value: monster.Value.Name
                );
                string jsonToSend = JsonConvert.SerializeObject(addLocalizedStringDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await _httpClient.PostAsync("http://localhost:5067/api/v1/monster/name", stringContent);
                Console.WriteLine(response.StatusCode);
            }
        }
    }

    private async Task AddMonstersToApi(string[] monsterSourceDirectories)
    {
        // Retrieve the ItemJsons from the french source file
        Dictionary<int, MonsterJson>? rawMonsterData = null;
        string? frenchDirectory = monsterSourceDirectories.FirstOrDefault(x => x.Contains("_fr"));
        if (frenchDirectory != null)
            rawMonsterData = ParseMonsterFiles(frenchDirectory);

        if (rawMonsterData != null)
            foreach (KeyValuePair<int, MonsterJson> monster in rawMonsterData)
            {
                // Add Item
                AddMonsterDto addMonsterDto = new AddMonsterDto(
                    Id: monster.Key,
                    Image: monster.Value.GfxId,
                    Breed: monster.Value.BreedId,
                    Ecosystem: null
                );
                string jsonToSend = JsonConvert.SerializeObject(addMonsterDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("http://localhost:5067/api/v1/monster", stringContent);
            }
    }

    private Dictionary<int, MonsterJson> ParseMonsterFiles(string folderPath)
    {
        Dictionary<int, MonsterJson> result = new Dictionary<int, MonsterJson>();
        string[] files = Directory.GetFiles(folderPath, "*.as");
        foreach (string file in files)
            ParseFileForMonsterData(file);
        return result;
        
        void ParseFileForMonsterData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("M["))
                {
                    // Extract the integer between square braces
                    Match match = Regex.Match(line, @"M\[(\d+)\]");
                    if (match.Success)
                    {
                        int key = int.Parse(match.Groups[1].Value);
                        // Extract and parse the JSON object starting from "M["
                        int startIndex = line.IndexOf('{');
                        int endIndex = line.LastIndexOf('}');
                        if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                        {
                            string jsonString = line.Substring(startIndex, endIndex - startIndex + 1);
                            string processedString = ProcessString(jsonString);
                            MonsterJson? monsterJson = JsonConvert.DeserializeObject<MonsterJson>(processedString);
                            if (monsterJson != null)
                                result[key] = monsterJson;
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