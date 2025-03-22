using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace test.Models;

public class Assumption
{
    public int Id { get; set; }
    [JsonPropertyName("level1")]
    [Column("Level1")]
    [JsonConverter(typeof(Assumption1Converter))]
    public Assumption1 Level1 { get; set; }
    [JsonPropertyName("level2")]
    [Column("Level2")]
    [JsonConverter(typeof(Assumption2Converter))]
    public Assumption2 Level2 { get; set; }
    [JsonPropertyName("level3")]
    [Column("Level3")]
    [JsonConverter(typeof(Assumption3Converter))]
    public Assumption3 Level3 { get; set; }
    [JsonPropertyName("value")]
    [Column("Value")]
    public double Value { get; set; }
}

public enum Assumption1
{
    A,
    B
}

public enum Assumption2
{
    HR,
    LR
}

public enum Assumption3
{
    Cash,
    Investment
}

public class Assumption1Converter : JsonConverter<Assumption1>
{
    public override Assumption1 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "A" => Assumption1.A,
            "B" => Assumption1.B,
            _ => throw new JsonException($"Invalid value for Level: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, Assumption1 value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}


public class Assumption2Converter : JsonConverter<Assumption2>
{
    public override Assumption2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "HR" => Assumption2.HR,
            "LR" => Assumption2.LR,
            _ => throw new JsonException($"Invalid value for Level: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, Assumption2 value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}

public class Assumption3Converter : JsonConverter<Assumption3>
{
    public override Assumption3 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "cash" => Assumption3.Cash,
            "Investment" => Assumption3.Investment,
            _ => throw new JsonException($"Invalid value for Level: {value}")
        };
    }

    public override void Write(Utf8JsonWriter writer, Assumption3 value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}