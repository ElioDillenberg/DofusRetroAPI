using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.JsonStructures;

public class EffectJson
{
    [JsonProperty("d")]
    public string Text {get; set; } = string.Empty;
}