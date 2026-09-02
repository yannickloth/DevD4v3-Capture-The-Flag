namespace Persistence.Tests.Common;

/// <summary>Resolves the location of the yesql SQL files used by the test fixtures.</summary>
/// <remarks>Change drivers: CD-19 ‖ CD-30 → CD-18; CD-18 (database schema/player data model: the SQL file layout)</remarks>
public class TestPaths
{
    /// <remarks>Change drivers: CD-19 ‖ CD-30 → CD-18; CD-18 (database schema/player data model)</remarks>
    public static string Sql =>
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "yesql");
}
