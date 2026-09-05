namespace Persistence.MariaDB;

using Persistence.MariaDB.Repositories.Player;
using Persistence.MariaDB.Repositories.TopPlayers;
using Persistence.MariaDB.Schema;
using Persistence.MariaDB.Settings;

[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Configuration, ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect)]
public static class PersistenceMariaDBServicesExtensions
{
    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Configuration, ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect)]
    public static IServiceCollection AddPersistenceMariaDBServices(
        this IServiceCollection services, 
        IConfiguration configuration,
        string sqlBasePath)
    {
        var mariadbSettings = configuration
            .GetRequiredSection("MariaDB")
            .Get<MariaDbSettings>();

        var connectionString = new MySqlConnectionStringBuilder()
        {
            Server   = mariadbSettings.Server,
            Port     = mariadbSettings.Port,
            Database = mariadbSettings.Database,
            UserID   = mariadbSettings.UserName,
            Password = mariadbSettings.Password
        }.ToString();

        mariadbSettings.ConnectionString = connectionString;
        services.AddSingleton(mariadbSettings)
                .AddSingleton<IPlayerRepository, PlayerRepository>()
                .AddSingleton<ITopPlayersRepository, TopPlayersRepository>();

        var sqlPath = Path.Combine(sqlBasePath, typeof(PersistenceMariaDBServicesExtensions).Namespace, "sql");
        ISqlCollection sqlCollection = new YeSqlLoader()
            .Exclude("schema.sql", "seed_data.sql")
            .LoadFromDirectories(sqlPath);

        var schemaFile = Path.Combine(sqlPath, "schema.sql");
        MariaDbSchemaExecutor.Execute(connectionString, schemaFile);
        services.AddSingleton(sqlCollection);
        return services;
    }
}
