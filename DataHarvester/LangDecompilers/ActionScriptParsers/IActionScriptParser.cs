namespace DataHarvester.LangDecompilers.ActionScriptParsers;

public interface IActionScriptParser
{
    // This would be the method that would parse the decompiled files and store them as DTos?
    public Task ParseDecompiledFiles();
}