namespace Persistence.Tests.Common;

/// <summary>Resolves the location of the yesql SQL files used by the test fixtures.</summary>
/// <remarks>Change drivers: CD-18 (database schema/player data model: the SQL file layout), CD-19 (MariaDB SQL dialect), CD-30 (SQLite SQL dialect: the yesql statement files consumed by the SQL providers)</remarks>
public class TestPaths
{
    /// <remarks>Change drivers: CD-18 (database schema/player data model), CD-19 (MariaDB SQL dialect), CD-30 (SQLite SQL dialect)</remarks>
    public static string Sql =>
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "yesql");
}
