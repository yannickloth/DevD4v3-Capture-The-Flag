namespace Persistence.SQLite;

using Persistence.SQLite.Repositories.Player;
using Persistence.SQLite.Repositories.TopPlayers;
using Persistence.SQLite.Schema;
using Persistence.SQLite.Settings;

[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Configuration, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect)]
public static class PersistenceSQLiteServicesExtensions
{
    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Configuration, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect)]
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
