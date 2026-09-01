namespace Persistence.MariaDB;

/// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-19 (SQL dialect/DBMS), CD-17 (game configuration/.env schema)</remarks>
internal class TopPlayersRepository(
    ISqlCollection sqlCollection,
    MariaDbSettings mariaDbSettings,
    TopPlayersSettings topPlayersSettings) : ITopPlayersRepository
{
    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-19 (SQL dialect/DBMS), CD-17 (game configuration/.env schema)</remarks>
    public IEnumerable<TopPlayersByMaxKillingSpree> GetByMaxKillingSpree(MaxTopPlayers maxPlayers)
    {
        using var connection = new MySqlConnection(mariaDbSettings.ConnectionString);
        connection.Open();

        MySqlCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["GetTopPlayersByMaxKillingSpree"];
        command.Parameters.AddWithValue("@required_max_killing_spree", topPlayersSettings.RequiredMaxKillingSpree);
        command.Parameters.AddWithValue("@max_players", maxPlayers.Value);

        using MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            yield return new TopPlayersByMaxKillingSpree
            {
                PlayerName = reader.GetString("name"),
                MaxKillingSpree = reader.GetInt32("max_killing_spree")
            };
        }
    }

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-19 (SQL dialect/DBMS), CD-17 (game configuration/.env schema)</remarks>
    public IEnumerable<TopPlayersByTotalKills> GetByTotalKills(MaxTopPlayers maxPlayers)
    {
        using var connection = new MySqlConnection(mariaDbSettings.ConnectionString);
        connection.Open();

        MySqlCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["GetTopPlayersByTotalKills"];
        command.Parameters.AddWithValue("@required_total_kills", topPlayersSettings.RequiredTotalKills);
        command.Parameters.AddWithValue("@max_players", maxPlayers.Value);

        using MySqlDataReader reader = command.ExecuteReader();
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
