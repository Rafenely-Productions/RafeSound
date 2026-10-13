using RafeSound.Application.Interfaces;
using RafeSound.Core.Engine;
using RafeSound.Core.Model;

namespace RafeSound.Application.Services;

public class ChordAnalysisService
{
    private readonly IChordFormulaRepository _formulaRepository;
    private ChordDetector? _detector;

    public ChordAnalysisService(IChordFormulaRepository formulaRepository)
    {
        _formulaRepository = formulaRepository;
    }

    public async Task<IReadOnlyList<ChordMatch>> AnalyzeVoicingAsync(Voicing voicing)
    {
        if (_detector == null)
        {
            var formulas = await _formulaRepository.GetAllAsync();
            _detector = new ChordDetector(formulas);
        }

        return _detector.DetectChords(voicing);
    }
}