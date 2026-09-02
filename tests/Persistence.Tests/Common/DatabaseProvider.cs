namespace Persistence.Tests.Common;

/// <summary>Enumerates the database providers the repository tests run against.</summary>
/// <remarks>Change drivers: CD-20 (outbound repository contract: what is exercised per provider), CD-19 (MariaDB SQL dialect), CD-30 (SQLite SQL dialect)</remarks>
public enum DatabaseProvider
{
    /// <summary>In-memory provider.</summary>
    /// <remarks>Change drivers: CD-20 (outbound repository contract, exercised dialect-free), CD-18 (database schema/player data model)</remarks>
    InMemory,
    /// <summary>MariaDB provider.</summary>
    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-19 (MariaDB SQL dialect)</remarks>
    MariaDb,
    /// <summary>SQLite provider.</summary>
    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-30 (SQLite SQL dialect)</remarks>
    Sqlite
}
