namespace RafeSound.Core.Model;

public readonly record struct Note(PitchClass PitchClass, int Octave)
{
    private const int SemitonesPerOctave = 12;
    private const int MidiOctaveOffset = 1;
    private const int A4MidiNumber = 69;
    private const double A4FrequencyHz = 440.0;
    private const double OctaveFrequencyRatio = 2.0;

    // Número MIDI absoluto (C-1 = 0, C4 = 60, A4 = 69)
    public int MidiNumber => (Octave + MidiOctaveOffset) * SemitonesPerOctave + PitchClass.Value;

    // Frecuencia acústica fundamental: f = 440 * 2^((midi - 69) / 12)
    public double FrequencyInHz =>
        A4FrequencyHz * Math.Pow(OctaveFrequencyRatio, (double)(MidiNumber - A4MidiNumber) / SemitonesPerOctave);

    public Note Transpose(int semitones)
    {
        return FromMidi(MidiNumber + semitones);
    }

    public static Note FromMidi(int midiNumber)
    {
        var octave = (midiNumber / SemitonesPerOctave) - MidiOctaveOffset;
        var pitch = new PitchClass(midiNumber % SemitonesPerOctave);
        return new Note(pitch, octave);
    }

    public override string ToString() => $"{PitchClass}{Octave}";
}