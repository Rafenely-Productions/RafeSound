using RafeSound.Core.Model;
using Xunit;

namespace RafeSound.Core.Tests.Model;

public class PitchClassTests
{
    [Theory]
    [InlineData(0, 0)]   // C
    [InlineData(12, 0)]  // C (una octava arriba se normaliza a 0)
    [InlineData(14, 2)]  // D
    [InlineData(-1, 11)] // B (hacia atrás)
    [InlineData(-12, 0)] // C
    public void PitchClass_NormalizesValueToModulo12(int input, int expected)
    {
        var pitch = new PitchClass(input);
        Assert.Equal(expected, pitch.Value);
    }

    [Fact]
    public void AdditionOperator_TransposesCorrectly()
    {
        var c = PitchClass.C;
        var result = c + 4; // 4 semitonos = E

        Assert.Equal(PitchClass.E, result);
    }

    [Fact]
    public void SemitonesTo_CalculatesShortestPositiveDistance()
    {
        var c = PitchClass.C;
        var g = PitchClass.G;

        // De C a G hay 7 semitonos (quinta justa)
        Assert.Equal(7, c.SemitonesTo(g));

        // De G a C hay 5 semitonos (cuarta justa ascendente)
        Assert.Equal(5, g.SemitonesTo(c));
    }
}