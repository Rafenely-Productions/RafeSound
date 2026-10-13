using SQLite;

namespace RafeSound.Infrastructure.Data.Entities;

[Table("Songs")]
public class SongEntity
{
    [PrimaryKey]
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string InstrumentName { get; set; } = string.Empty;
    public string ContentJson { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}