namespace Persistence.SQLite;

/// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-30 (SQLite SQL dialect) → CD-18; CD-17 (game configuration/.env schema) → CD-20</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): sqlCollection -> CD-18; sqliteSettings -> CD-17; topPlayersSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
internal class TopPlayersRepository(
    ISqlCollection sqlCollection,
    SQLiteSettings sqliteSettings,
    TopPlayersSettings topPlayersSettings) : ITopPlayersRepository
{
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-30 (SQLite SQL dialect) → CD-18; CD-17 (game configuration/.env schema) → CD-20</remarks>
    public IEnumerable<TopPlayersByMaxKillingSpree> GetByMaxKillingSpree(MaxTopPlayers maxPlayers)
    {
        using var connection = new SqliteConnection(sqliteSettings.ConnectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["GetTopPlayersByMaxKillingSpree"];
        command.Parameters.AddWithValue("$required_max_killing_spree", topPlayersSettings.RequiredMaxKillingSpree);
        command.Parameters.AddWithValue("$max_players", maxPlayers.Value);

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new TopPlayersByMaxKillingSpree
            {
                PlayerName = reader.GetString("name"),
                MaxKillingSpree = reader.GetInt32("max_killing_spree")
            };
        }
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-30 (SQLite SQL dialect) → CD-18; CD-17 (game configuration/.env schema) → CD-20</remarks>
    public IEnumerable<TopPlayersByTotalKills> GetByTotalKills(MaxTopPlayers maxPlayers)
    {
        using var connection = new SqliteConnection(sqliteSettings.ConnectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["GetTopPlayersByTotalKills"];
        command.Parameters.AddWithValue("$required_total_kills", topPlayersSettings.RequiredTotalKills);
        command.Parameters.AddWithValue("$max_players", maxPlayers.Value);

        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new TopPlayersByTotalKills
            {
                PlayerName = reader.GetString("name"),
                TotalKills = reader.GetInt32("total_kills"),
                Rank = (RankId)reader.GetInt32("rank_id")
            };
        }
    }
}
