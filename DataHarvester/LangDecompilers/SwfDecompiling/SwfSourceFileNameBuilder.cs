using ClassLibrary.Enums.Languages;

namespace DataHarvester.LangDecompilers.SwfDecompiling;

public enum SwfSourceFileType
{
    Items,
    ItemStats,
    ItemSets,
    ItemEffects,
    Recipes,
    Monsters
}

public static class SwfSourceFileNameBuilder
{
    public static Dictionary<SwfSourceFileType, string> TypePrefixes;

    public static Dictionary<Language, string> LanguageElements; 
    
    static SwfSourceFileNameBuilder()
    {
        if (TypePrefixes == null || TypePrefixes.Count == 0)
            TypePrefixes = new Dictionary<SwfSourceFileType, string>
            {
                { SwfSourceFileType.Items, "items_" },
                { SwfSourceFileType.ItemStats, "itemstats_" },
                { SwfSourceFileType.ItemSets, "itemsets_" },
                { SwfSourceFileType.ItemEffects, "effects_" },
                { SwfSourceFileType.Recipes, "crafts_"},
                { SwfSourceFileType.Monsters, "monsters_"}
            };
        if (LanguageElements == null || LanguageElements.Count == 0)
            LanguageElements = new  Dictionary<Language, string>
            {
                { Language.FR, "fr" },
                { Language.EN, "en" },
                { Language.ES, "es" }
            };
    }
    
    public static string GenerateSwfRegex(SwfSourceFileType sourceFileType, Language sourceFileLanguage)
    {
        // Construct the complete regex pattern for SWF files
        string regexPattern = $@"^({TypePrefixes[sourceFileType]})({LanguageElements[sourceFileLanguage]})_\d+\.swf$";

        return regexPattern;
    }
}