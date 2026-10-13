namespace RafeSound.Core.Model;

public enum StringState
{
    Open,       // Traste 0
    Fretted,    // Trastes 1..N
    Muted       // X (no suena)
}

public readonly record struct FretboardPosition(int StringIndex, int Fret, StringState State)
{
    public static FretboardPosition Open(int stringIndex) =>
        new(stringIndex, 0, StringState.Open);

    public static FretboardPosition Muted(int stringIndex) =>
        new(stringIndex, -1, StringState.Muted);

    public static FretboardPosition AtFret(int stringIndex, int fret) =>
        fret == 0 ? Open(stringIndex) : new(stringIndex, fret, StringState.Fretted);

    public bool ProducesSound => State != StringState.Muted;
}