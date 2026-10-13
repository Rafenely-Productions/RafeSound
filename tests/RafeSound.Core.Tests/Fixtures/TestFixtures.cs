using RafeSound.Core.Model;

namespace RafeSound.Core.Tests;

public static class TestFixtures
{
    public static readonly Tuning UkuleleStandardTuning = new(
        "Standard High-G",
        new[]
        {
            new Note(PitchClass.G, 4), // Cuerda 4 (High-G)
            new Note(PitchClass.C, 4), // Cuerda 3
            new Note(PitchClass.E, 4), // Cuerda 2
            new Note(PitchClass.A, 4)  // Cuerda 1
        }
    );

    public static readonly InstrumentDescriptor Ukulele = new(
        Name: "Ukulele",
        StringCount: 4,
        TotalFrets: 12,
        DefaultTuning: UkuleleStandardTuning
    );

    public static readonly IReadOnlyList<ChordFormula> StandardFormulas = new[]
    {
        new ChordFormula("Major", "", new HashSet<int> { 0, 4, 7 }),
        new ChordFormula("Minor", "m", new HashSet<int> { 0, 3, 7 }),
        new ChordFormula("Dominant 7th", "7", new HashSet<int> { 0, 4, 7, 10 }),
        new ChordFormula("Major 7th", "maj7", new HashSet<int> { 0, 4, 7, 11 }),
        new ChordFormula("Minor 7th", "m7", new HashSet<int> { 0, 3, 7, 10 }),
        new ChordFormula("Major 6th", "6", new HashSet<int> { 0, 4, 7, 9 })
    };
}