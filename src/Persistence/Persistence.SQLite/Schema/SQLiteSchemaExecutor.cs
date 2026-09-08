namespace CTF.Application.SchemaNs5;

[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.Configuration)]
internal static class SQLiteSchemaExecutor
{
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.Configuration)]
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
