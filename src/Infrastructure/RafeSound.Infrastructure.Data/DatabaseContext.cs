using RafeSound.Infrastructure.Data.Entities;
using SQLite;

namespace RafeSound.Infrastructure.Data;

public class DatabaseContext
{
    private readonly SQLiteAsyncConnection _database;
    private bool _initialized;

    public DatabaseContext(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
    }

    public async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        if (!_initialized)
        {
            await _database.CreateTableAsync<InstrumentEntity>();
            await _database.CreateTableAsync<ChordFormulaEntity>();
            await _database.CreateTableAsync<SongEntity>();

            await DataSeeder.SeedInitialDataAsync(_database);
            _initialized = true;
        }

        return _database;
    }
}