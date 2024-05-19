using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using ClassLibrary.Enums.Languages;

namespace DataHarvester.LangDecompilers.SwfDecompiling;

public class SwfDecompiler
{
    private readonly string _relativeDecompiledScriptsDestinationFolder = "\\LangDecompilers\\DecompiledSwfFiles";
    
    public void DecompileFiles(SwfSourceFileType sourceFileType, Language language)
    {
        StringBuilder stringBuilder = new StringBuilder();
        
        // Build source SWF directory path
        stringBuilder.Append(Directory.GetCurrentDirectory());
        stringBuilder.Append("\\LangDecompilers\\PyLangGetter\\data\\lang\\swf\\");
        string sourceSwfDirectoryPath = stringBuilder.ToString();
        
        // Build the file pattern (regex) for the SWF file
        string targetFilePattern = SwfSourceFileNameBuilder.GenerateSwfRegex(sourceFileType, language);
        
        // Get the path of the swfFile to decompile
        string[] swfFiles = Directory.GetFiles(sourceSwfDirectoryPath)
            .Where(file => Regex.IsMatch(Path.GetFileName(file.ToString()), targetFilePattern, RegexOptions.IgnoreCase))
            .ToArray();
        
        // Build destination path
        stringBuilder.Clear();
        stringBuilder.Append(Directory.GetCurrentDirectory());
        stringBuilder.Append(_relativeDecompiledScriptsDestinationFolder);
        string destinationPath = stringBuilder.ToString();
        
        // Decompile the SWF file
        DecompileSwfFile(destinationPath, swfFiles[0]);
        
        // Rename destination folder
        Directory.Move($"{destinationPath}\\scripts", $"{destinationPath}\\{SwfSourceFileNameBuilder.TypePrefixes[sourceFileType]}{SwfSourceFileNameBuilder.LanguageElements[language]}");
    }
    
    private void DecompileSwfFile(string destination, string source)
    {
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = $"java",
                Arguments = $"-jar ffdec.jar -export script \"{destination}\" \"{source}\"",
                RedirectStandardOutput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = $"{Directory.GetCurrentDirectory()}\\LangDecompilers\\FFDec"
            }
        };

        process.Start();
        process.WaitForExit();
    }
}