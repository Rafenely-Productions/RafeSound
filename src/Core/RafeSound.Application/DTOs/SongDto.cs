namespace RafeSound.Application.DTOs;

public record SongSectionDto(
    string Title,          // Ej. "Verse", "Chorus", "Bridge"
    string Lyrics,
    IReadOnlyList<string> Chords
);

public record SongDto(
    Guid Id,
    string Title,
    string Artist,
    string InstrumentName,
    IReadOnlyList<SongSectionDto> Sections,
    DateTime CreatedAt,
    DateTime UpdatedAt
);