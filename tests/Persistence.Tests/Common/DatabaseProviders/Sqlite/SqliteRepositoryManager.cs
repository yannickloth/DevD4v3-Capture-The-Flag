namespace CTF.Application.Tests.Repositories.SqliteDialect.DatabaseSchema.Composition.BCrypt;

/// <summary>Wires the SQLite persistence stack for the repository tests.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
public class SqliteRepositoryManager : IRepositoryManager
{
    private readonly ISqlCollection _seedSqlCollection;
    private readonly ServiceProvider _serviceProvider;

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public IPlayerRepository PlayerRepository { get; }
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public ITopPlayersRepository TopPlayersRepository { get; }
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-30 (SQLite SQL dialect) → CD-20; CD-18 (database schema/player data model) → CD-20; CD-21 (DI container/composition) → CD-20; CD-25 (BCrypt password-hashing contract) → CD-20</remarks>
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

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public void Dispose()
    {
        _serviceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public void InitializeSeedData() => ExecuteCommand("InitializeSeedData");
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public void RemoveSeedData() => ExecuteCommand("RemoveSeedData");

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
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
