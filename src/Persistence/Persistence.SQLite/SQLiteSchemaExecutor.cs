namespace Persistence.SQLite;

/// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18; CD-17 (game configuration/.env schema) → CD-18</remarks>
internal static class SQLiteSchemaExecutor
{
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-30 (SQLite SQL dialect) → CD-18; CD-17 (game configuration/.env schema) → CD-18</remarks>
    public static void Execute(string connectionString, string schemaFile)
    {
        if (!File.Exists(schemaFile))
            throw new FileNotFoundException(
                "The SQLite schema file was not found.",
                schemaFile);

        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        connection.CreateRegexpFunction();

        using var command = connection.CreateCommand();
        command.CommandText = File.ReadAllText(schemaFile);
        command.ExecuteNonQuery();
    }
}
