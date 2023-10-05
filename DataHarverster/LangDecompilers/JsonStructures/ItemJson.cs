using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataHarvester.LangDecompilers.JsonStructures;

public partial class ItemJson
{
    [JsonProperty("n")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("nn")]
    public string AllCapsName { get; set; } = string.Empty;
    
    [JsonProperty("d")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("t")]
    public int ItemType { get; set; }
    
    [JsonProperty("w")]
    public int Pods { get; set; }
    
    [JsonProperty("l")]
    public int Level { get; set; }
    
    [JsonProperty("fm")]
    public bool SmithMageable { get; set; }

    [JsonProperty("e")]
    public E[] WeaponCharacteristics { get; set; } = null!;

    [JsonProperty("c")]
    public string Conditions { get; set; } = string.Empty;

    // Note: this id is built in a way that each ItemCategory would have it's set of Gfx,
    // effectively several items could have the same GfxId but if they are of different ItemTypes, the Gfx is different
    [JsonProperty("g")]
    public int GfxId { get; set; }
    
    [JsonProperty("p")]
    public int Price { get; set; }
    
    [JsonProperty("wd")]
    public bool IsEquipment { get; set; }
    
    [JsonProperty("ep")]
    public int Ep { get; set; }
    
    [JsonProperty("an")]
    public int An { get; set; }
}

public struct E
{
    public bool? Bool;
    public long? Integer;

    public static implicit operator E(bool boolean) => new E { Bool = boolean };
    public static implicit operator E(long integer) => new E { Integer = integer };
}

public partial class ItemJson
{
    public static ItemJson? FromJson(string json) =>
        JsonConvert.DeserializeObject<ItemJson>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this ItemJson self) => JsonConvert.SerializeObject(self, Converter.Settings);
}

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            EConverter.Singleton,
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };
}

internal class EConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(E) || t == typeof(E?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.Integer:
                var integerValue = serializer.Deserialize<long>(reader);
                return new E { Integer = integerValue };
            case JsonToken.Boolean:
                var boolValue = serializer.Deserialize<bool>(reader);
                return new E { Bool = boolValue };
        }
        throw new Exception("Cannot unmarshal type E");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        var value = (E)untypedValue;
        if (value.Integer != null)
        {
            serializer.Serialize(writer, value.Integer.Value);
            return;
        }
        if (value.Bool != null)
        {
            serializer.Serialize(writer, value.Bool.Value);
            return;
        }
        throw new Exception("Cannot marshal type E");
    }

    public static readonly EConverter Singleton = new EConverter();
}