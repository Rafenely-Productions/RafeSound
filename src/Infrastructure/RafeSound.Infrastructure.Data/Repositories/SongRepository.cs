using System.Text.Json;
using RafeSound.Application.DTOs;
using RafeSound.Application.Interfaces;
using RafeSound.Infrastructure.Data.Entities;

namespace RafeSound.Infrastructure.Data.Repositories;

public class SongRepository(DatabaseContext context) : ISongRepository
{
    private readonly DatabaseContext _context = context;

    public async Task<IReadOnlyList<SongDto>> GetAllAsync()
    {
        var db = await _context.GetDatabaseAsync();
        var entities = await db.Table<SongEntity>()
            .OrderByDescending(s => s.UpdatedAt)
            .ToListAsync();

        return [.. entities.Select(MapToDto)];
    }

    public async Task<SongDto?> GetByIdAsync(Guid id)
    {
        var db = await _context.GetDatabaseAsync();
        var entity = await db.Table<SongEntity>().FirstOrDefaultAsync(s => s.Id == id);

        return entity != null ? MapToDto(entity) : null;
    }

    public async Task SaveAsync(SongDto song)
    {
        var db = await _context.GetDatabaseAsync();
        var entity = new SongEntity
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            InstrumentName = song.InstrumentName,
            ContentJson = JsonSerializer.Serialize(song.Sections),
            CreatedAt = song.CreatedAt == default ? DateTime.UtcNow : song.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };

        await db.InsertOrReplaceAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var db = await _context.GetDatabaseAsync();
        await db.DeleteAsync<SongEntity>(id);
    }

    private static SongDto MapToDto(SongEntity entity)
    {
        var sections = JsonSerializer.Deserialize<List<SongSectionDto>>(entity.ContentJson)
                       ?? [];

        return new SongDto(
            Id: entity.Id,
            Title: entity.Title,
            Artist: entity.Artist,
            InstrumentName: entity.InstrumentName,
            Sections: sections,
            CreatedAt: entity.CreatedAt,
            UpdatedAt: entity.UpdatedAt
        );
    }
}