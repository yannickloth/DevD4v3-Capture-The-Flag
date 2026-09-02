namespace Persistence.Tests.Common;

/// <summary>Dispatches to the correct repository-manager implementation per provider.</summary>
/// <remarks>Change drivers: CD-20 (root; outbound repository contract: the repository seam per provider); CD-19 (MariaDB SQL dialect) → CD-20; CD-30 (SQLite SQL dialect) → CD-20; CD-21 (DI container/composition) → CD-20</remarks>
public class RepositoryManagerFactory
{
    /// <summary>Creates the repository manager for a provider.</summary>
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-19 (MariaDB SQL dialect) → CD-20; CD-30 (SQLite SQL dialect) → CD-20; CD-21 (DI container/composition) → CD-20</remarks>
    public static IRepositoryManager Create(DatabaseProvider provider) => provider switch
    {
        DatabaseProvider.InMemory => new InMemoryRepositoryManager(),
        DatabaseProvider.MariaDb => new MariaDbRepositoryManager(),
        DatabaseProvider.Sqlite => new SqliteRepositoryManager(),
        _ => throw new NotSupportedException($"'{provider}' was not found as a database provider.")
    };
}
