namespace Persistence.Tests.Common;

/// <summary>Enumerates the database providers the repository tests run against.</summary>
/// <remarks>Change drivers: CD-20 (root; outbound repository contract: what is exercised per provider); CD-19 (MariaDB SQL dialect) → CD-20; CD-30 (SQLite SQL dialect) → CD-20</remarks>
public enum DatabaseProvider
{
    /// <summary>In-memory provider.</summary>
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract, exercised dialect-free); CD-18 (database schema/player data model) → CD-20</remarks>
    InMemory,
    /// <summary>MariaDB provider.</summary>
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-19 (MariaDB SQL dialect) → CD-20</remarks>
    MariaDb,
    /// <summary>SQLite provider.</summary>
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-30 (SQLite SQL dialect) → CD-20</remarks>
    Sqlite
}
