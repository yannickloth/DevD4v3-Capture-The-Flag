namespace Persistence.SQLite.Extensions;

[ChangeDriversAttribute(ChangeDriver.SqliteDialect)]
internal static class SqliteDataReaderExtensions
{
    [ChangeDriversAttribute(ChangeDriver.SqliteDialect)]
    public static string GetString(this SqliteDataReader reader, string name)
        => reader.GetString(reader.GetOrdinal(name));

    [ChangeDriversAttribute(ChangeDriver.SqliteDialect)]
    public static int GetInt32(this SqliteDataReader reader, string name)
        => reader.GetInt32(reader.GetOrdinal(name));

    [ChangeDriversAttribute(ChangeDriver.SqliteDialect)]
    public static DateTime GetDateTime(this SqliteDataReader reader, string name)
        => reader.GetDateTime(reader.GetOrdinal(name));
}
