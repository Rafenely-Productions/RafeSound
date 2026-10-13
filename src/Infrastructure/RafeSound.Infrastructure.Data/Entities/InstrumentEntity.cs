using SQLite;

namespace RafeSound.Infrastructure.Data.Entities;

[Table("Instruments")]
public class InstrumentEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed(Unique = true)]
    public string Name { get; set; } = string.Empty;
    public int StringCount { get; set; }
    public int TotalFrets { get; set; }
    public string TuningName { get; set; } = string.Empty;

    // Serializado como JSON simple: "[{\"PitchClass\":7,\"Octave\":4}, ...]"
    public string OpenNotesJson { get; set; } = string.Empty;
}