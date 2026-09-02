namespace Persistence.MariaDB;

/// <remarks>Change drivers: CD-18 (database schema/player data model), CD-19 (MariaDB SQL dialect), CD-17 (game configuration/.env schema)</remarks>
internal class MariaDbSchemaExecutor
{
    /// <remarks>Change drivers: CD-18 (database schema/player data model), CD-19 (MariaDB SQL dialect), CD-17 (game configuration/.env schema)</remarks>
    public static void Execute(string connectionString, string schemaFile)
    {
        if (!File.Exists(schemaFile))
            throw new FileNotFoundException(
                "The MariaDB schema file was not found.",
                schemaFile);

        // Create a temporary connection string without selecting a database.
        var builder = new MySqlConnectionStringBuilder(connectionString)
        {
            Database = string.Empty
        };

        using var connection = new MySqlConnection(builder.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = File.ReadAllText(schemaFile);
        command.ExecuteNonQuery();
    }
}
