using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.DTOs.Items.ItemDto;
using ClassLibrary.DTOs.Localization;
using ClassLibrary.Enums.ItemConditions;
using ClassLibrary.Enums.Languages;
using DataHarvester.LangDecompilers.JsonStructures;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class ItemActionScriptParser : ActionScriptParserBase
{
    public override async Task ParseDecompiledFiles()
    {
        // Get all directories in the decompiled files directory that start with "items_"
        string[] itemSourceDirectories = Directory.GetDirectories($"{_decompiledFilesDirectoryBasePath}", "items_*");
        
        // Add the frame_1 folder to the path
        for (int i = 0; i < itemSourceDirectories.Length; i++)
            itemSourceDirectories[i] = String.Concat(itemSourceDirectories[i], "\\frame_1");

        // Add the Items to the API
        await AddItemsToApi(itemSourceDirectories);

        // Add the ItemConditions to the API
        await AddItemConditionsToApi(itemSourceDirectories);
        
        // Add the localized names and descriptions to the API
        // await AddLocalizedNameAndDescriptionToApi(itemSourceDirectories, Language.FR, "_fr");
        // await AddLocalizedNameAndDescriptionToApi(itemSourceDirectories, Language.EN, "_en");
        // await AddLocalizedNameAndDescriptionToApi(itemSourceDirectories, Language.ES, "_es");
    }

    private async Task AddItemsToApi(string[] itemSourceDirectories)
    {
        // Retrieve the ItemJsons from the french source file
        Dictionary<int, ItemJson>? rawItemData = null;
        string? frenchDirectory = itemSourceDirectories.FirstOrDefault(x => x.Contains("_fr"));
        if (frenchDirectory != null)
            rawItemData = ParseItemFiles(frenchDirectory);
        
        // Now map all the ItemJson objects to AddItemDto objects to send them to the API
        if (rawItemData != null)
        {
            foreach (KeyValuePair<int, ItemJson> item in rawItemData)
            {
                // Add Item
                AddItemDto addItemDto = new AddItemDto(
                    Id: item.Key,
                    ItemType: item.Value.ItemType,
                    Level: item.Value.Level,
                    Pods: item.Value.Pods,
                    Price: item.Value.Price / 10, // For some reason we need to divide the price by 10 to get the price in kamas
                    Image: item.Value.GfxId
                );
                string jsonToSend = JsonConvert.SerializeObject(addItemDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("http://localhost:5067/api/v1/item", stringContent);
            }
        }
    }

    private async Task AddItemConditionsToApi(string[] itemSourceDirectories)
    {
        // Retrieve the ItemJsons from the french source file
        Dictionary<int, ItemJson>? rawItemData = null;
        string? frenchDirectory = itemSourceDirectories.FirstOrDefault(x => x.Contains("_fr"));
        if (frenchDirectory != null)
            rawItemData = ParseItemFiles(frenchDirectory);
        
        // Now map all the ItemJson objects to AddItemDto objects to send them to the API
        if (rawItemData != null)
        {
            foreach (KeyValuePair<int, ItemJson> item in rawItemData)
            {
                // Check for conditions on item
                if (item.Value.Conditions != null)
                {
                    // Retrieve all conditions as AddItemDto objects
                    AddItemConditionDto[] addItemConditionDtos = ParseConditions(item.Value.Conditions, item.Key);
                    
                    // Send all the conditions to the API
                    foreach (AddItemConditionDto addItemConditionDto in addItemConditionDtos)
                    {
                        string jsonToSend = JsonConvert.SerializeObject(addItemConditionDto);
                        StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseMessage = await _httpClient.PostAsync("http://localhost:5067/api/v1/item/condition", stringContent);
                        Console.WriteLine(responseMessage.StatusCode);
                    }
                }
            }
        }
    }
    
    private readonly char[] _conditionSigns = { '>', '<', '=', '!' };
    private readonly string[] _separators = { "&", "|" };
    private AddItemConditionDto[] ParseConditions(string conditionString, int itemId)
    {
        
        // Split the condition string based on '&' and '|'
        string[] conditions = conditionString.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
        
        // Clean the condition strings
        for (int i = 0; i < conditions.Length; i++)
        {
            conditions[i] = conditions[i].Trim();
            conditions[i] = conditions[i].Replace("(","").Replace(")","");
        }
        
        // Prepare result value to be filled
        List<AddItemConditionDto> result = new List<AddItemConditionDto>();
        
        foreach (string condition in conditions)
        {
            // Check if one of the condition signs is present in the string, otherwise skip this condition
            if (!condition.Any(c => _conditionSigns.Contains(c)))
            {
                Console.WriteLine($"condition {condition} does not contain a recognized condition sign, skipping");
                continue;
            }
            
            // Set the limits of each section of the condition
            int endConditionTypeIndex = condition.IndexOfAny(_conditionSigns);
            int endConditionSigneIndex = endConditionTypeIndex + 1;
            
            // Extract the condition type, sign, and value
            string conditionTypeString = condition.Substring(0, endConditionTypeIndex); // Extract the first two characters (e.g., "CI")
            string conditionSignString = condition.Substring(endConditionTypeIndex, 1); // Extract the third character (e.g., ">")
            string conditionValueString = condition.Substring(endConditionTypeIndex + 1, condition.Length - endConditionSigneIndex); // Extract the rest as the value (e.g., "60")

            // Parse condition type and sign
            ConditionType? conditionType = ConditionStrings.ConditionTypeStrings.FirstOrDefault(x => x.Value == conditionTypeString).Key;
            if (conditionType == null)
            {
                Console.WriteLine($"Condition type {conditionTypeString} was not recognized, skipping");
                continue;
            }
            
            ConditionSign? conditionSignType = ConditionStrings.ConditionSignStrings.FirstOrDefault(x => x.Value == conditionSignString).Key;
            if (conditionSignType == null)
            {
                Console.WriteLine($"Condition Sign type {conditionSignString} was not recognized, skipping");
                continue;
            }

            if (!int.TryParse(conditionValueString, out int conditionValue))
            {
                Console.WriteLine($"Condition value {conditionValueString} could not be parsed as an integer, skipping");
                continue;
            }
                
            AddItemConditionDto addItemConditionDto = new AddItemConditionDto
            (
                ItemId: itemId,
                ConditionType: (int)conditionType,
                ConditionSignType: (int)conditionSignType,
                Value: conditionValue
            );
            result.Add(addItemConditionDto);
        }
        return result.ToArray();
    }

    private async Task AddLocalizedNameAndDescriptionToApi(
        string[] itemSourceDirectories,
        Language language,
        string languageFileExtension)
    {
        // Retrieve the ItemJsons from the french source file
        Dictionary<int, ItemJson>? rawItemData = null;
        string? languageDirectory = itemSourceDirectories.FirstOrDefault(x => x.Contains(languageFileExtension));
        if (languageDirectory != null)
            rawItemData = ParseItemFiles(languageDirectory);

        // Now map all the ItemJson objects to AddItemDto objects to send them to the API
        if (rawItemData != null)
        {
            foreach (KeyValuePair<int, ItemJson> item in rawItemData)
            {
                // Add ItemName
                AddLocalizedStringDto addLocalizedStringDto = new AddLocalizedStringDto(
                    EntityId: item.Key,
                    LanguageId: (int)language,
                    Value: item.Value.Name
                );
                string jsonToSend = JsonConvert.SerializeObject(addLocalizedStringDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await _httpClient.PostAsync("http://localhost:5067/api/v1/item/name", stringContent);
                Console.WriteLine(response.StatusCode);

                // Add Item Description
                addLocalizedStringDto = new AddLocalizedStringDto(
                    EntityId: item.Key,
                    LanguageId: (int)language,
                    Value: item.Value.Description
                );
                jsonToSend = JsonConvert.SerializeObject(addLocalizedStringDto);
                stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                response = await _httpClient.PostAsync("http://localhost:5067/api/v1/item/description", stringContent);
                Console.WriteLine(response.StatusCode);
            }
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