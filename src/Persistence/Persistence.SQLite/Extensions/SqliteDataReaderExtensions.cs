namespace Persistence.SQLite.Extensions;

/// <remarks>Change drivers: CD-30 (root; root; SQLite SQL dialect)</remarks>
internal static class SqliteDataReaderExtensions
{
    /// <remarks>Change drivers: CD-30 (root; root; SQLite SQL dialect)</remarks>
    public static string GetString(this SqliteDataReader reader, string name)
        => reader.GetString(reader.GetOrdinal(name));

    /// <remarks>Change drivers: CD-30 (root; root; SQLite SQL dialect)</remarks>
    public static int GetInt32(this SqliteDataReader reader, string name)
        => reader.GetInt32(reader.GetOrdinal(name));

    /// <remarks>Change drivers: CD-30 (root; root; SQLite SQL dialect)</remarks>
    public static DateTime GetDateTime(this SqliteDataReader reader, string name)
        => reader.GetDateTime(reader.GetOrdinal(name));
}
