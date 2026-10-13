namespace RafeSound.Core.Model;

public record ChordMatch(
    PitchClass Root,
    ChordFormula Formula,
    PitchClass? BassNote = null,
    bool IsInversion = false
)
{
    /// <summary>
    /// Nombre para mostrar (ej: "C", "Am", "G7", "C/E" si es inversión).
    /// </summary>
    public string DisplayName
    {
        get
        {
            var baseName = $"{Root.ToDisplayString()}{Formula.Suffix}";
            if (IsInversion && BassNote.HasValue && BassNote.Value != Root)
            {
                return $"{baseName}/{BassNote.Value.ToDisplayString()}";
            }
            return baseName;
        }
    }

    public override string ToString() => DisplayName;
}