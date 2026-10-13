using RafeSound.Core.Model;

namespace RafeSound.Application.Interfaces;

public interface IInstrumentRepository
{
    Task<IReadOnlyList<InstrumentDescriptor>> GetAllAsync();
    Task<InstrumentDescriptor?> GetByNameAsync(string name);
}