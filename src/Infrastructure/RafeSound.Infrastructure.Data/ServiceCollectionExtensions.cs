using Microsoft.Extensions.DependencyInjection;
using RafeSound.Application.Interfaces;
using RafeSound.Infrastructure.Data.Repositories;

namespace RafeSound.Infrastructure.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRafeSoundData(this IServiceCollection services, string databasePath)
    {
        services.AddSingleton(new DatabaseContext(databasePath));
        services.AddScoped<IInstrumentRepository, InstrumentRepository>();
        services.AddScoped<IChordFormulaRepository, ChordFormulaRepository>();
        services.AddScoped<ISongRepository, SongRepository>();

        return services;
    }
}