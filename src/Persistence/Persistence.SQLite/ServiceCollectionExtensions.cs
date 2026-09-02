namespace Persistence.SQLite;

/// <remarks>Change drivers: CD-17 (game configuration/.env schema); CD-18 (database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18; CD-21 (DI container/composition)</remarks>
public static class PersistenceSQLiteServicesExtensions
{
    /// <remarks>Change drivers: CD-17 (game configuration/.env schema); CD-18 (database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18; CD-21 (DI container/composition)</remarks>
    public static IServiceCollection AddPersistenceSQLiteServices(
        this IServiceCollection services, 
        IConfiguration configuration,
        string sqlBasePath)
    {
        var sqliteSettings = configuration
            .GetRequiredSection("SQLite")
            .Get<SQLiteSettings>();

        var connectionString = new SqliteConnectionStringBuilder()
        {
            DataSource = sqliteSettings.DataSource
        }.ToString();

        sqliteSettings.ConnectionString = connectionString;
        services.AddSingleton(sqliteSettings)
                .AddSingleton<IPlayerRepository, PlayerRepository>()
                .AddSingleton<ITopPlayersRepository, TopPlayersRepository>();

        var sqlPath = Path.Combine(sqlBasePath, typeof(PersistenceSQLiteServicesExtensions).Namespace, "sql");
        ISqlCollection sqlCollection = new YeSqlLoader()
            .Exclude("schema.sql", "seed_data.sql")
            .LoadFromDirectories(sqlPath);

        var schemaFile = Path.Combine(sqlPath, "schema.sql");
        SQLiteSchemaExecutor.Execute(connectionString, schemaFile);
        services.AddSingleton(sqlCollection);
        return services;
    }
}
