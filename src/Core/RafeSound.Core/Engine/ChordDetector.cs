namespace RafeSound.Core.Engine;

using RafeSound.Core.Model;

public class ChordDetector
{
    private readonly IReadOnlyList<ChordFormula> _formulas;

    public ChordDetector(IEnumerable<ChordFormula> formulas)
    {
        ArgumentNullException.ThrowIfNull(formulas);
        _formulas = [.. formulas];
    }

    /// <summary>
    /// Analiza un Voicing tocado en el mástil y devuelve todos los acordes posibles ordenados por relevancia.
    /// </summary>
    public IReadOnlyList<ChordMatch> DetectChords(Voicing voicing)
    {
        var soundingNotes = voicing.GetSoundingNotes();
        if (soundingNotes.Count == 0)
            return [];

        var uniquePitches = voicing.GetUniquePitchClasses();
        if (uniquePitches.Count < 2)
            return []; // Una sola nota no forma acorde (se requiere al menos díada/tríada)

        // En instrumentos como ukulele con High-G la cuerda física más baja o la frecuencia más grave
        var lowestSoundingPitch = soundingNotes
            .OrderBy(n => n.MidiNumber)
            .First()
            .PitchClass;

        var matches = new List<ChordMatch>();

        // Probar cada tono único como posible fundamental (Root)
        foreach (var candidateRoot in uniquePitches)
        {
            // Calcular los intervalos respecto a este candidateRoot
            var intervals = uniquePitches
                .Select(p => candidateRoot.SemitonesTo(p))
                .ToHashSet();

            // Buscar si coincide con alguna fórmula conocida
            foreach (var formula in _formulas)
            {
                if (formula.Intervals.SetEquals(intervals))
                {
                    bool isInversion = lowestSoundingPitch != candidateRoot;

                    matches.Add(new ChordMatch(
                        Root: candidateRoot,
                        Formula: formula,
                        BassNote: lowestSoundingPitch,
                        IsInversion: isInversion
                    ));
                }
            }
        }

        // Ordenar: primero los acordes en posición fundamental (no inversiones), luego por simplicidad
        return [.. matches
            .OrderBy(m => m.IsInversion)
            .ThenBy(m => m.Formula.Intervals.Count)];
    }
}