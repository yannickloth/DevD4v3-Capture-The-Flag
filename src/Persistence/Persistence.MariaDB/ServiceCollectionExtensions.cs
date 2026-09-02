namespace Persistence.MariaDB;

/// <remarks>Change drivers: CD-21 (root; DI container/composition); CD-17 (game configuration/.env schema) → CD-21; CD-18 (database schema/player data model) → CD-21; CD-19 (MariaDB SQL dialect) → CD-18</remarks>
public static class PersistenceMariaDBServicesExtensions
{
    /// <remarks>Change drivers: CD-21 (root; DI container/composition); CD-17 (game configuration/.env schema) → CD-21; CD-18 (database schema/player data model) → CD-21; CD-19 (MariaDB SQL dialect) → CD-18</remarks>
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
