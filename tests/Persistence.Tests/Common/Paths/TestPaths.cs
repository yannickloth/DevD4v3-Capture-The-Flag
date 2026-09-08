namespace CTF.Application.Tests.Schema.MariaDbDialect.SqliteDialect;

/// <summary>Resolves the location of the yesql SQL files used by the test fixtures.</summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
public class TestPaths
{
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
    public static string Sql =>
        Path.Combine(
            Directory.GetCurrentDirectory(),
            "yesql");
}
