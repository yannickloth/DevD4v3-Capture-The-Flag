namespace Persistence.Tests.Common;

/// <summary>Resolves the location of the yesql SQL files used by the test fixtures.</summary>
/// <remarks>Change drivers: CD-22 (root; hosting/deployment spec: map test-data paths); CD-26 (NUnit test-framework contract) → CD-22; CD-27 (FluentAssertions contract) → CD-22; CD-11 (map configuration) → CD-22</remarks>
public class TestPaths
{
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-19 (MariaDB SQL dialect) → CD-18; CD-30 (SQLite SQL dialect) → CD-18</remarks>
    public static string Sql =>
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "yesql");
}
