namespace Persistence.Tests.Common.Contracts;

/// <summary>Enumerates the database providers the repository tests run against.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
public enum DatabaseProvider
{
    /// <summary>In-memory provider.</summary>
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema)]
    InMemory,
    /// <summary>MariaDB provider.</summary>
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect)]
    MariaDb,
    /// <summary>SQLite provider.</summary>
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect)]
    Sqlite
}
