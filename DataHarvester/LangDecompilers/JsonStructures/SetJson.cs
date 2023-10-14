using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.JsonStructures;

public class SetJson
{
    [JsonProperty("n")]
    public string Name {get; set; } = string.Empty;

    [JsonProperty("i")]
    public int[] EquipmentIds { get; set; } = null!;
}