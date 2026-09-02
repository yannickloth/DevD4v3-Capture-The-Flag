namespace Persistence.Tests.Common;

/// <summary>Dispatches to the correct repository-manager implementation per provider.</summary>
/// <remarks>Change drivers: CD-19 ‖ CD-30 → CD-18; CD-20 (outbound repository contract); CD-21 (DI container/composition); CD-29 (code-under-test: the repository seam per provider)</remarks>
public class RepositoryManagerFactory
{
    /// <summary>Creates the repository manager for a provider.</summary>
    /// <remarks>Change drivers: CD-19 ‖ CD-30 → CD-18; CD-20 (outbound repository contract); CD-21 (DI container/composition)</remarks>
    public static IRepositoryManager Create(DatabaseProvider provider) => provider switch
    {
        DatabaseProvider.InMemory => new InMemoryRepositoryManager(),
        DatabaseProvider.MariaDb => new MariaDbRepositoryManager(),
        DatabaseProvider.Sqlite => new SqliteRepositoryManager(),
        _ => throw new NotSupportedException($"'{provider}' was not found as a database provider.")
    };
}
