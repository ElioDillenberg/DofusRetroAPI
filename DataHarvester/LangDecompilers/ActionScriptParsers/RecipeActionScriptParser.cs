using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.DTOs.Ingredients;
using ClassLibrary.DTOs.Recipe;
using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public class RecipeActionScriptParser : ActionScriptParserBase
{
    public override async Task ParseDecompiledFiles()
    {
        string[] itemSourceDirectories = GetItemSourceDirectories("crafts_*");
        
        Dictionary<int, Tuple<int, int>[]>? recipes = null;
        string? frenchDirectory = itemSourceDirectories.FirstOrDefault(x => x.Contains("_fr"));
        if (frenchDirectory != null)
            recipes = ParseRecipeFiles(frenchDirectory);
        
        if (recipes != null)
        {
            foreach (KeyValuePair<int, Tuple<int,int>[]> recipe in recipes)
            {
                // Build the ingredients list
                List<AddIngredientDto> ingredients = new List<AddIngredientDto>();
                foreach (Tuple<int, int> tuple in recipe.Value)
                {
                    AddIngredientDto addIngredientDto = new AddIngredientDto(
                        ItemId: tuple.Item1,
                        Quantity: tuple.Item2
                    );
                    ingredients.Add(addIngredientDto);
                }
                
                // Build the AddRecipeDto
                AddRecipeDto addRecipeDto = new AddRecipeDto(
                    ItemId: recipe.Key,
                    Ingredients : ingredients
                );
                
                Console.WriteLine($"addRecipeDto.ItemId = {addRecipeDto.ItemId}");
                foreach (AddIngredientDto ingredient in addRecipeDto.Ingredients)
                    Console.WriteLine($"ingredient.ItemId = {ingredient.ItemId}, ingredient.Quantity = {ingredient.Quantity}");
            
                // Send the data to the API
                string jsonToSend = JsonConvert.SerializeObject(addRecipeDto);
                StringContent stringContent = new StringContent(jsonToSend, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5067/api/v1/recipe", stringContent);
            }    
        }
    }
    
    private Dictionary<int, Tuple<int, int>[]> ParseRecipeFiles(string folderPath)
    {
        Dictionary<int, Tuple<int, int>[]> result = new Dictionary<int, Tuple<int, int>[]>();
        string[] files = Directory.GetFiles(folderPath, "*.as");
        foreach (string file in files)
            ParseFileForRecipeData(file);
        return result;

        void ParseFileForRecipeData(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (line.StartsWith("CR["))
                {
                    // Extract the integer between square braces
                    Match match = Regex.Match(line, @"CR\[(\d+)\]");
                    if (match.Success)
                    {
                        int key = int.Parse(match.Groups[1].Value);
                        // Extract and parse the JSON object starting from "I.u["
                        int startIndex = line.IndexOf("[[", StringComparison.Ordinal);
                        int endIndex = line.LastIndexOf("]]", StringComparison.Ordinal);
                        if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
                        {
                            string dataString = line.Substring(startIndex, endIndex - startIndex + 1);
                            Tuple<int, int>[] tuples = ParseTuples(dataString);
                            result.Add(key, tuples);
                            Console.WriteLine("Parsed recipe");
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ChatGPT generated
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Tuple<int, int>[] ParseTuples(string input)
    {
        // Remove outer brackets
        input = input.Trim('[', ']');

        // Split the string into individual tuples
        string[] tupleStrings = input.Split(new[] { "],[" }, StringSplitOptions.None);

        // Parse each tuple and store it in Tuple<int, int> array
        List<Tuple<int, int>> tuples = new List<Tuple<int, int>>();
        foreach (var tupleString in tupleStrings)
        {
            string[] values = tupleString.Split(',');

            // Parse the two integers in the tuple
            if (values.Length == 2 && int.TryParse(values[0], out int first) && int.TryParse(values[1], out int second))
                tuples.Add(Tuple.Create(first, second));
            else
                Console.WriteLine($"Error parsing tuple: {tupleString}");
        }

        return tuples.ToArray();
    }
}