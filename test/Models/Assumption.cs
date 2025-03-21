using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace test.Models;

public class Assumption
{
    public int Id { get; set; }
    [JsonPropertyName("level1")]
    [Column("Level1")]
    public string Level1 { get; set; }
    [JsonPropertyName("level2")]
    [Column("Level2")]
    public string Level2 { get; set; }
    [JsonPropertyName("level3")]
    [Column("Level3")]
    public string Level3 { get; set; }
    [JsonPropertyName("value")]
    [Column("Value")]
    public double Value { get; set; }
}