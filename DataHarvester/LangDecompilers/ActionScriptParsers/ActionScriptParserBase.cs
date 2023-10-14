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
    
    public abstract Task ParseDecompiledFiles();
}