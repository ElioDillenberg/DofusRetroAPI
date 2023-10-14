using ClassLibrary.DTOs.Items.ItemConditionDto;
using ClassLibrary.Enums.ItemConditions;

namespace DataHarvester;

public static class SandBox
{
    // public static AddItemConditionDto[] ParseConditionsSandbox(string singleStringConditions, int itemId)
    // {
    //     List<AddItemConditionDto> result = new List<AddItemConditionDto>();
    //     string[] conditions = singleStringConditions.Split('&');
    //     foreach (string condition in conditions)
    //     {
    //         Console.WriteLine(condition);
    //     }
    //     return result.ToArray();
    // }
    
    public static AddItemConditionDto[] ParseConditionString(string conditionString, int itemId)
    {
        string[] separators = { "&", "|" };
        
        // Split the condition string based on '&'
        string[] conditions = conditionString.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        
        // Clean the condition strings
        for (int i = 0; i < conditions.Length; i++)
        {
            conditions[i] = conditions[i].Trim();
            conditions[i] = conditions[i].Replace("(","").Replace(")","");
        }
        
        List<AddItemConditionDto> result = new List<AddItemConditionDto>();
        
        char[] conditionSigns = { '>', '<', '=', '!' };
        
        foreach (string condition in conditions)
        {
            // Check if one of the condition signs is present in the string, otherwise skip this condition
            if (!condition.Any(c => conditionSigns.Contains(c)))
            {
                Console.WriteLine($"condition {condition} does not contain a recognized condition sign, skipping");
                continue;
            }
            
            int endConditionTypeIndex = condition.IndexOfAny(conditionSigns);
            int endConditionSigneIndex = endConditionTypeIndex + 1;
            // Extract the condition type, sign, and value
            string conditionTypeString = condition.Substring(0, endConditionTypeIndex); // Extract the first two characters (e.g., "CI")
            string conditionSignString = condition.Substring(endConditionTypeIndex, 1); // Extract the third character (e.g., ">")
            string conditionValueString = condition.Substring(endConditionTypeIndex + 1, condition.Length - endConditionSigneIndex); // Extract the rest as the value (e.g., "60")
            Console.WriteLine($"condition: {condition}");
            Console.WriteLine($"conditionTypeString:{conditionTypeString}");
            Console.WriteLine($"conditionSignString:{conditionSignString}");
            Console.WriteLine($"conditionValueString:{conditionValueString}\n\n");

            // Parse condition type and sign
            ConditionType? conditionType = ConditionStrings.ConditionTypeStrings.FirstOrDefault(x => x.Value == conditionTypeString).Key;
            if (conditionType == null)
            {
                Console.WriteLine($"Condition type {conditionTypeString} was not recognized, skipping");
                continue;
            }
            
            ConditionSign? conditionSign = ConditionStrings.ConditionSignStrings.FirstOrDefault(x => x.Value == conditionSignString).Key;
            if (conditionSign == null)
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
                ConditionSignType: (int)conditionSign,
                Value: conditionValue
            );
            
            result.Add(addItemConditionDto);
        }
        return result.ToArray();
    }
}