namespace RafeSound.Core.Model;

public readonly record struct PitchClass
{
    public const int SemitonesPerOctave = 12;

    public int Value { get; }

    public PitchClass(int value)
    {
        // Normaliza cualquier entero al rango cromático [0..11]
        Value = (value % SemitonesPerOctave + SemitonesPerOctave) % SemitonesPerOctave;
    }

    public static readonly PitchClass C = new(0);
    public static readonly PitchClass Db = new(1);
    public static readonly PitchClass D = new(2);
    public static readonly PitchClass Eb = new(3);
    public static readonly PitchClass E = new(4);
    public static readonly PitchClass F = new(5);
    public static readonly PitchClass Gb = new(6);
    public static readonly PitchClass G = new(7);
    public static readonly PitchClass Ab = new(8);
    public static readonly PitchClass A = new(9);
    public static readonly PitchClass Bb = new(10);
    public static readonly PitchClass B = new(11);

    public static PitchClass operator +(PitchClass pitch, int semitones) =>
        new(pitch.Value + semitones);

    public static PitchClass operator -(PitchClass pitch, int semitones) =>
        new(pitch.Value - semitones);

    public int SemitonesTo(PitchClass target) =>
        (target.Value - Value + SemitonesPerOctave) % SemitonesPerOctave;

    public string ToDisplayString(bool preferSharps = true) => Value switch
    {
        0 => "C",
        1 => preferSharps ? "C#" : "Db",
        2 => "D",
        3 => preferSharps ? "D#" : "Eb",
        4 => "E",
        5 => "F",
        6 => preferSharps ? "F#" : "Gb",
        7 => "G",
        8 => preferSharps ? "G#" : "Ab",
        9 => "A",
        10 => preferSharps ? "A#" : "Bb",
        11 => "B",
        _ => "?"
    };

    public override string ToString() => ToDisplayString();
}