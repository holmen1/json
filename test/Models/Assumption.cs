using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models;

public class Assumption
{
    public int Id { get; set; }
    [Column("Level")]
    public string Level { get; set; }
    [Column("SubLevel")]
    public string SubLevel { get; set; }
    [Column("Kategory")]
    public string Kategory { get; set; }
    [Column("Value")]
    public double Value { get; set; }
}