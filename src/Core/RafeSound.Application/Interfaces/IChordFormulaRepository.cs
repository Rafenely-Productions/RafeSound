using RafeSound.Core.Model;

namespace RafeSound.Application.Interfaces;

public interface IChordFormulaRepository
{
    Task<IReadOnlyList<ChordFormula>> GetAllAsync();
}