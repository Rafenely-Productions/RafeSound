namespace RafeSound.Core.Model;

public record ChordFormula(
    string Name,
    string Suffix,
    IReadOnlySet<int> Intervals
);