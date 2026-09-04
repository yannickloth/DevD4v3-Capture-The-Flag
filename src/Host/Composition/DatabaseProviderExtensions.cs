namespace CTF.Host.Composition;

/// <remarks>Change drivers: CD-21 (root; DI container/composition: provider selection); CD-17 (game configuration/.env schema: DatabaseProvider) → CD-21; CD-22 (hosting/deployment spec: yesql path) → CD-21</remarks>
public static class DatabaseProviderExtensions
{
    /// <remarks>Change drivers: CD-21 (root; DI container/composition: provider selection); CD-17 (game configuration/.env schema: DatabaseProvider) → CD-21; CD-22 (hosting/deployment spec: yesql path) → CD-21</remarks>
    public static void ChooseDatabaseProvider(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string sqlPath = GameModePaths.Sql;
        var providers = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            { "InMemory", () => services.AddPersistenceInMemoryServices() },
            { "SQLite",   () => services.AddPersistenceSQLiteServices(configuration, sqlPath) },
            { "MariaDb",  () => services.AddPersistenceMariaDBServices(configuration, sqlPath) },
        };

        string selectedProvider = configuration["DatabaseProvider"];
        if (providers.TryGetValue(selectedProvider, out Action addPersistenceServices))
        {
            addPersistenceServices();
            Console.WriteLine($"[CTF.Host:INFO] This database provider has been selected: {selectedProvider}");
            return;
        }

        throw new NotSupportedException($"Provider '{selectedProvider}' is not supported");
    }
}
