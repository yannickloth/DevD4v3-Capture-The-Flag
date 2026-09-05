namespace Persistence.Tests.Common.Factory;

/// <summary>Dispatches to the correct repository-manager implementation per provider.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect, ChangeDriver.Composition)]
public class RepositoryManagerFactory
{
    /// <summary>Creates the repository manager for a provider.</summary>
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect, ChangeDriver.Composition)]
    public static IRepositoryManager Create(DatabaseProvider provider) => provider switch
    {
        DatabaseProvider.InMemory => new InMemoryRepositoryManager(),
        DatabaseProvider.MariaDb => new MariaDbRepositoryManager(),
        DatabaseProvider.Sqlite => new SqliteRepositoryManager(),
        _ => throw new NotSupportedException($"'{provider}' was not found as a database provider.")
    };
}
