using SQLite;

namespace RafeSound.Infrastructure.Data.Entities;

[Table("ChordFormulas")]
public class ChordFormulaEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Suffix { get; set; } = string.Empty;

    // Serializado como JSON o lista delimitada: "0,4,7"
    public string IntervalsCsv { get; set; } = string.Empty;
}