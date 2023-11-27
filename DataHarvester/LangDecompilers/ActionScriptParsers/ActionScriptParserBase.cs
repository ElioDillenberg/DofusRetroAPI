namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public abstract class ActionScriptParserBase : IActionScriptParser
{
    protected readonly string _decompiledFilesDirectoryBasePath;

    protected readonly HttpClient _httpClient;
    
    public ActionScriptParserBase()
    {
        _decompiledFilesDirectoryBasePath = $"{Directory.GetCurrentDirectory()}\\LangDecompilers\\DecompiledSwfFiles";
        _httpClient = new HttpClient();
    }
    
    protected string[] GetItemSourceDirectories(string searchPattern)
    {
        // Get all directories in the decompiled files directory that start with "items_"
        string[] itemSourceDirectories = Directory.GetDirectories($"{_decompiledFilesDirectoryBasePath}", searchPattern);
        // Add the frame_1 folder to the path
        for (int i = 0; i < itemSourceDirectories.Length; i++)
            itemSourceDirectories[i] = String.Concat(itemSourceDirectories[i], "\\frame_1");
        return itemSourceDirectories;
    }
    
    public abstract Task ParseDecompiledFiles();
}