namespace Persistence.SQLite.Extensions;

/// <remarks>Change drivers: CD-30 (SQLite SQL dialect)</remarks>
public static class SqliteConnectionExtensions
{
    /// <summary>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/user-defined-functions#operators">
    /// See user-defined functions.
    /// </see>
    /// </summary>
    /// <remarks>Change drivers: CD-30 (SQLite SQL dialect)</remarks>
    public static void CreateRegexpFunction(this SqliteConnection connection)
    {
        connection.CreateFunction(
            name: "regexp",
            function: (string pattern, string input) => Regex.IsMatch(input, pattern)
        );
    }
}
