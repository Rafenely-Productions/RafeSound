using System.Linq;
using RafeSound.Core.Engine;
using RafeSound.Core.Model;
using RafeSound.Core.Tests; // O RafeSound.Core.Tests.Fixtures según donde guardaste el archivo
using Xunit;

namespace RafeSound.Core.Tests.Engine;

public class ChordDetectorTests
{
    private readonly ChordDetector _detector = new(TestFixtures.StandardFormulas);
    private readonly InstrumentDescriptor _uke = TestFixtures.Ukulele;

    private Voicing CreateUkeVoicing(params int[] frets)
    {
        var positions = frets
            .Select((fret, index) => FretboardPosition.AtFret(index, fret))
            .ToList();

        return new Voicing(_uke, _uke.DefaultTuning, positions);
    }

    [Theory]
    [InlineData(0, 0, 0, 3, "C")]    // C (en estado fundamental)
    [InlineData(2, 0, 1, 0, "F")]    // F (físicamente suena F/C)
    [InlineData(2, 0, 0, 0, "Am")]   // Am (físicamente suena Am/C)
    [InlineData(0, 2, 1, 2, "G7")]   // G7 (físicamente suena G7/D)
    public void DetectChords_ValidVoicings_ReturnsExpectedPrimaryChord(
    int s4, int s3, int s2, int s1, string expectedBaseChord)
    {
        var voicing = CreateUkeVoicing(s4, s3, s2, s1);
        var matches = _detector.DetectChords(voicing);

        Assert.NotEmpty(matches);
        var match = matches[0];

        // Validamos el nombre armónico base (Root + Suffix)
        var baseChordName = $"{match.Root.ToDisplayString()}{match.Formula.Suffix}";
        Assert.Equal(expectedBaseChord, baseChordName);
    }

    [Fact]
    public void DetectChords_AllOpenStrings_DetectsBothC6AndAm7()
    {
        // [0, 0, 0, 0] -> Notas {C, E, G, A}
        var voicing = CreateUkeVoicing(0, 0, 0, 0);

        var matches = _detector.DetectChords(voicing);

        Assert.Contains(matches, m => m.DisplayName == "C6");
        Assert.Contains(matches, m => m.Root == PitchClass.A && m.Formula.Suffix == "m7");
    }
}