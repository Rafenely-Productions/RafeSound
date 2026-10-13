using RafeSound.Application.Interfaces;
using RafeSound.Core.Model;
using RafeSound.Infrastructure.Data.Entities;

namespace RafeSound.Infrastructure.Data.Repositories;

public class ChordFormulaRepository(DatabaseContext context) : IChordFormulaRepository
{
    private readonly DatabaseContext _context = context;

    public async Task<IReadOnlyList<ChordFormula>> GetAllAsync()
    {
        var db = await _context.GetDatabaseAsync();
        var entities = await db.Table<ChordFormulaEntity>().ToListAsync();

        return [.. entities.Select(MapToDomain)];
    }

    private static ChordFormula MapToDomain(ChordFormulaEntity entity)
    {
        var intervals = entity.IntervalsCsv
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToHashSet();

        return new ChordFormula(
            Name: entity.Name,
            Suffix: entity.Suffix,
            Intervals: intervals
        );
    }
}