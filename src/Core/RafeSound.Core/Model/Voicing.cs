namespace RafeSound.Core.Model;

public class Voicing
{
    public InstrumentDescriptor Instrument { get; }
    public Tuning Tuning { get; }
    public IReadOnlyList<FretboardPosition> Positions { get; }

    public Voicing(
        InstrumentDescriptor instrument,
        Tuning tuning,
        IReadOnlyList<FretboardPosition> positions)
    {
        Instrument = instrument;
        Tuning = tuning;
        Positions = positions;
    }

    /// <summary>
    /// Devuelve las notas activas que suenan en este acorde.
    /// </summary>
    public IReadOnlyList<Note> GetSoundingNotes()
    {
        var notes = new List<Note>();

        foreach (var pos in Positions)
        {
            if (!pos.ProducesSound)
                continue;

            var openStringNote = Tuning.OpenNotes[pos.StringIndex];
            notes.Add(openStringNote.Transpose(pos.Fret));
        }

        return notes;
    }

    /// <summary>
    /// Devuelve las clases de tono únicas (sin duplicados de octava),
    /// esenciales para que el motor identifique el acorde.
    /// </summary>
    public IReadOnlySet<PitchClass> GetUniquePitchClasses() =>
        GetSoundingNotes().Select(n => n.PitchClass).ToHashSet();
}