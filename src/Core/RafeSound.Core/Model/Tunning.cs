namespace RafeSound.Core.Model;

public record Tuning(
    string Name,
    IReadOnlyList<Note> OpenNotes
);