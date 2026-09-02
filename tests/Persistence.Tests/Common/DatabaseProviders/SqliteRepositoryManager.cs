namespace Persistence.Tests.Common.DatabaseProviders;

/// <summary>Wires the SQLite persistence stack for the repository tests.</summary>
/// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract); CD-20 (outbound repository contract); CD-18 (database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18; CD-21 (DI container/composition); CD-29 (code-under-test: the SQLite repository seam)</remarks>
public class SqliteRepositoryManager : IRepositoryManager
{
    private readonly ISqlCollection _seedSqlCollection;
    private readonly ServiceProvider _serviceProvider;

    /// <remarks>Change drivers: CD-20 (outbound repository contract)</remarks>
    public IPlayerRepository PlayerRepository { get; }
    /// <remarks>Change drivers: CD-20 (outbound repository contract)</remarks>
    public ITopPlayersRepository TopPlayersRepository { get; }
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract); CD-20 (outbound repository contract); CD-18 (database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18; CD-21 (DI container/composition)</remarks>
    public SqliteRepositoryManager()
    {
        var services = new ServiceCollection();
        IConfiguration configuration = new ConfigurationBuilder()
            .AddEnvFile(".env.test", optional: false)
            .Build();

        services.AddSingleton(new TopPlayersSettings());
        services.AddSingleton<IPasswordHasher, FakePasswordHasher>();
        services.AddPersistenceSQLiteServices(configuration, TestPaths.Sql);
        _serviceProvider = services.BuildServiceProvider();

        var sqlFile = Path.Combine(
            TestPaths.Sql, 
            typeof(PersistenceSQLiteServicesExtensions).Namespace, 
            "sql",
            "seed_data.sql"
        );

        _seedSqlCollection = new YeSqlLoader()
            .LoadFromFiles(sqlFile);

        PlayerRepository = _serviceProvider.GetRequiredService<IPlayerRepository>();
        TopPlayersRepository = _serviceProvider.GetRequiredService<ITopPlayersRepository>();
    }

    /// <remarks>Change drivers: CD-21 (DI container/composition)</remarks>
    public void Dispose()
    {
        _serviceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <remarks>Change drivers: CD-18 (database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18</remarks>
    public void InitializeSeedData() => ExecuteCommand("InitializeSeedData");
    /// <remarks>Change drivers: CD-18 (database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18</remarks>
    public void RemoveSeedData() => ExecuteCommand("RemoveSeedData");

    /// <remarks>Change drivers: CD-18 (database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18</remarks>
    private void ExecuteCommand(string tagName)
    {
        var settings = _serviceProvider.GetRequiredService<SQLiteSettings>();
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();
        connection.CreateRegexpFunction();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = _seedSqlCollection[tagName];
        command.ExecuteNonQuery();
    }
}
