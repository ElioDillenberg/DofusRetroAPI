using Newtonsoft.Json;

namespace DataHarvester.LangDecompilers.JsonStructures;

public partial class MonsterJson
{
    [JsonProperty("n")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("b")]
    public int BreedId { get; set; }
    
    [JsonProperty("s")]
    public bool Summoned { get; set; }
    
    [JsonProperty("a")]
    public int Alignment { get; set; }
    
    [JsonProperty("g")]
    public int GfxId { get; set; }

    [JsonProperty("g1")]
    public MonsterCharacteristics? Characteristics1 { get; set; }

    [JsonProperty("g2")]
    public MonsterCharacteristics? Characteristics2 { get; set; }
    
    [JsonProperty("g3")]
    public MonsterCharacteristics? Characteristics3 { get; set; }
    
    [JsonProperty("g4")]
    public MonsterCharacteristics? Characteristics4 { get; set; }
    
    [JsonProperty("g5")]
    public MonsterCharacteristics? Characteristics5 { get; set; }
    
    [JsonProperty("g6")]
    public MonsterCharacteristics? Characteristics6 { get; set; }
    
    [JsonProperty("g7")]
    public MonsterCharacteristics? Characteristics7 { get; set; }
    
    [JsonProperty("g8")]
    public MonsterCharacteristics? Characteristics8 { get; set; }
    
    [JsonProperty("g9")]
    public MonsterCharacteristics? Characteristics9 { get; set; }
    
    [JsonProperty("g10")]
    public MonsterCharacteristics? Characteristics10 { get; set; }
    
    public MonsterCharacteristics?[] Characteristics => new MonsterCharacteristics?[]
    {
        SetRank(Characteristics1, 1),
        SetRank(Characteristics2, 2),
        SetRank(Characteristics3, 3),
        SetRank(Characteristics4, 4),
        SetRank(Characteristics5, 5),
        SetRank(Characteristics6, 6),
        SetRank(Characteristics7, 7),
        SetRank(Characteristics8, 8),
        SetRank(Characteristics9, 9),
        SetRank(Characteristics10, 10)
    };
    
    private MonsterCharacteristics? SetRank(MonsterCharacteristics? monsterCharacteristics, int rank)
    {
        if (monsterCharacteristics == null)
            return null;
        monsterCharacteristics.Rank = rank;
        return monsterCharacteristics;
    }
}

public class MonsterCharacteristics
{
    public int Rank { get; set; }
    
    [JsonProperty("l")]
    public int Level { get; set; }

    // [0] = res neutre, [1] res terre, [2] res feu, [3] res eau, [4] res air, [5] esquive PA, [6] esquive PM
    [JsonProperty("r")]
    public int[] Resistances { get; set; } = new int[7];
    
    [JsonProperty("ap")]
    public int ActionPoints { get; set; }
    
    [JsonProperty("mp")]
    public int MovementPoints { get; set; }
}

