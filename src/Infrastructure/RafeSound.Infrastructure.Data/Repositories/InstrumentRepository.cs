using System.Text.Json;
using RafeSound.Application.Interfaces;
using RafeSound.Core.Model;
using RafeSound.Infrastructure.Data.Entities;
using SQLite;

namespace RafeSound.Infrastructure.Data.Repositories;

public class InstrumentRepository(DatabaseContext context) : IInstrumentRepository
{
    private readonly DatabaseContext _context = context;

    public async Task<IReadOnlyList<InstrumentDescriptor>> GetAllAsync()
    {
        var db = await _context.GetDatabaseAsync();
        var entities = await db.Table<InstrumentEntity>().ToListAsync();

        IReadOnlyList<InstrumentDescriptor> value = [.. entities.Select(MapToDomain)];
        return value;
    }

    public async Task<InstrumentDescriptor?> GetByNameAsync(string name)
    {
        var db = await _context.GetDatabaseAsync();
        var entity = await db.Table<InstrumentEntity>()
            .FirstOrDefaultAsync(e => e.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

        return entity != null ? MapToDomain(entity) : null;
    }

    private static InstrumentDescriptor MapToDomain(InstrumentEntity entity)
    {
        var openNotes = JsonSerializer.Deserialize<List<Note>>(entity.OpenNotesJson)
                         ?? [];

        var tuning = new Tuning(entity.TuningName, openNotes);

        return new InstrumentDescriptor(
            Name: entity.Name,
            StringCount: entity.StringCount,
            TotalFrets: entity.TotalFrets,
            DefaultTuning: tuning
        );
    }
}