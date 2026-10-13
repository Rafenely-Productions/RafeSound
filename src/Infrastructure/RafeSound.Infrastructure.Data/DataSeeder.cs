using System.Text.Json;
using RafeSound.Core.Model;
using RafeSound.Infrastructure.Data.Entities;
using SQLite;

namespace RafeSound.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedInitialDataAsync(SQLiteAsyncConnection database)
    {
        // 1. Semilla de Instrumentos
        if (await database.Table<InstrumentEntity>().CountAsync() == 0)
        {
            var ukeNotes = new List<Note>
            {
                new(PitchClass.G, 4),
                new(PitchClass.C, 4),
                new(PitchClass.E, 4),
                new(PitchClass.A, 4)
            };

            var guitarNotes = new List<Note>
            {
                new(PitchClass.E, 2),
                new(PitchClass.A, 2),
                new(PitchClass.D, 3),
                new(PitchClass.G, 3),
                new(PitchClass.B, 3),
                new(PitchClass.E, 4)
            };

            await database.InsertAllAsync(new[]
            {
                new InstrumentEntity
                {
                    Name = "Ukulele",
                    StringCount = 4,
                    TotalFrets = 12,
                    TuningName = "Standard High-G",
                    OpenNotesJson = JsonSerializer.Serialize(ukeNotes)
                },
                new InstrumentEntity
                {
                    Name = "Guitar",
                    StringCount = 6,
                    TotalFrets = 21,
                    TuningName = "Standard",
                    OpenNotesJson = JsonSerializer.Serialize(guitarNotes)
                }
            });
        }

        // 2. Semilla de Fórmulas de Acordes
        if (await database.Table<ChordFormulaEntity>().CountAsync() == 0)
        {
            var defaultFormulas = new[]
            {
                new ChordFormulaEntity { Name = "Major", Suffix = "", IntervalsCsv = "0,4,7" },
                new ChordFormulaEntity { Name = "Minor", Suffix = "m", IntervalsCsv = "0,3,7" },
                new ChordFormulaEntity { Name = "Diminished", Suffix = "dim", IntervalsCsv = "0,3,6" },
                new ChordFormulaEntity { Name = "Augmented", Suffix = "aug", IntervalsCsv = "0,4,8" },
                new ChordFormulaEntity { Name = "Suspended 4th", Suffix = "sus4", IntervalsCsv = "0,5,7" },
                new ChordFormulaEntity { Name = "Suspended 2nd", Suffix = "sus2", IntervalsCsv = "0,2,7" },
                new ChordFormulaEntity { Name = "Dominant 7th", Suffix = "7", IntervalsCsv = "0,4,7,10" },
                new ChordFormulaEntity { Name = "Major 7th", Suffix = "maj7", IntervalsCsv = "0,4,7,11" },
                new ChordFormulaEntity { Name = "Minor 7th", Suffix = "m7", IntervalsCsv = "0,3,7,10" },
                new ChordFormulaEntity { Name = "Major 6th", Suffix = "6", IntervalsCsv = "0,4,7,9" },
                new ChordFormulaEntity { Name = "Minor 6th", Suffix = "m6", IntervalsCsv = "0,3,7,9" }
            };

            await database.InsertAllAsync(defaultFormulas);
        }
    }
}