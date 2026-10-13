namespace RafeSound.Core.Model;

public record InstrumentDescriptor(
    string Name,
    int StringCount,
    int TotalFrets,
    Tuning DefaultTuning
);