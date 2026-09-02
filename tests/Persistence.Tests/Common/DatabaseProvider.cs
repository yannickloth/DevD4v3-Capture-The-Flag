namespace Persistence.Tests.Common;

/// <summary>Enumerates the database providers the repository tests run against.</summary>
/// <remarks>Change drivers: CD-19 (MariaDB SQL dialect), CD-30 (SQLite SQL dialect), CD-20 (outbound repository contract: what is exercised per provider). The InMemory member is dialect-free (no SQL).</remarks>
public enum DatabaseProvider
{
    /// <summary>In-memory provider.</summary>
    /// <remarks>Change drivers: CD-20 (outbound repository contract, exercised dialect-free); CD-18 (database schema/player data model).</remarks>
    InMemory,
    /// <summary>MariaDB provider.</summary>
    /// <remarks>Change drivers: CD-19 (MariaDB SQL dialect), CD-20 (outbound repository contract).</remarks>
    MariaDb,
    /// <summary>SQLite provider.</summary>
    /// <remarks>Change drivers: CD-30 (SQLite SQL dialect), CD-20 (outbound repository contract).</remarks>
    Sqlite
}
