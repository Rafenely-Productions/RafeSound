using RafeSound.Application.DTOs;

namespace RafeSound.Application.Interfaces;

public interface ISongRepository
{
    Task<IReadOnlyList<SongDto>> GetAllAsync();
    Task<SongDto?> GetByIdAsync(Guid id);
    Task SaveAsync(SongDto song);
    Task DeleteAsync(Guid id);
}