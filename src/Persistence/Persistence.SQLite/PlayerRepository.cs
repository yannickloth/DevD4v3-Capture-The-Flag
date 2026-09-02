namespace Persistence.SQLite;

/// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect), CD-21 (DI container/composition)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): passwordHasher -> CD-25; sqlCollection -> CD-18; settings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
internal class PlayerRepository(
    IPasswordHasher passwordHasher,
    ISqlCollection sqlCollection,
    SQLiteSettings settings) : IPlayerRepository
{
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void Create(PlayerInfo player)
    {
        var passwordHash = passwordHasher.HashPassword(player.Password);
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();
        connection.CreateRegexpFunction();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["CreatePlayer"];
        command.Parameters.AddWithValue("$name",              player.Name);
        command.Parameters.AddWithValue("$password",          passwordHash);
        command.Parameters.AddWithValue("$total_kills",       player.TotalKills);
        command.Parameters.AddWithValue("$total_deaths",      player.TotalDeaths);
        command.Parameters.AddWithValue("$max_killing_spree", player.MaxKillingSpree);
        command.Parameters.AddWithValue("$brought_flags",     player.BroughtFlags);
        command.Parameters.AddWithValue("$captured_flags",    player.CapturedFlags);
        command.Parameters.AddWithValue("$dropped_flags",     player.DroppedFlags);
        command.Parameters.AddWithValue("$returned_flags",    player.ReturnedFlags);
        command.Parameters.AddWithValue("$head_shots",        player.HeadShots);
        command.Parameters.AddWithValue("$gungame_wins",      player.GunGameWins);
        command.Parameters.AddWithValue("$role_id",           player.RoleId);
        command.Parameters.AddWithValue("$skin_id",           player.SkinId);
        command.Parameters.AddWithValue("$rank_id",           player.RankId);
        command.Parameters.AddWithValue("$created_at",        player.CreatedAt);
        command.Parameters.AddWithValue("$last_connection",   player.LastConnection);
        int id = (int)(long)command.ExecuteScalar();

        // The Account ID is immutable and lacks a public setter; Reflection is used to modify it.
        player.SetValue(value: id, propertyName: nameof(PlayerInfo.AccountId));
    }

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public bool Exists(string name)
    {
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();
        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["PlayerExists"];
        command.Parameters.AddWithValue("$name", name);
        using SqliteDataReader reader = command.ExecuteReader();
        return reader.HasRows;
    }

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public PlayerInfo GetOrDefault(string name)
    {
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["GetPlayerByName"];
        command.Parameters.AddWithValue("$name", name);

        using SqliteDataReader reader = command.ExecuteReader();
        bool notExists = !reader.Read();
        if (notExists)
            return default;

        var playerInfo = new PlayerInfo();
        // The public setter is used only for plaintext passwords.
        // For that reason, we use Reflection to set the already encrypted password.
        playerInfo.SetValue(value: reader.GetString("password"), propertyName: nameof(PlayerInfo.Password));

        playerInfo.SetName(reader.GetString("name"));
        playerInfo.SetTotalKills(reader.GetInt32("total_kills"));
        playerInfo.SetTotalDeaths(reader.GetInt32("total_deaths"));
        playerInfo.SetMaxKillingSpree(reader.GetInt32("max_killing_spree"));
        playerInfo.SetRole((RoleId)reader.GetInt32("role_id"));
        playerInfo.SetRank((RankId)reader.GetInt32("rank_id"));
        playerInfo.SetSkin(reader.GetInt32("skin_id"));

        // Reflection is used here because these properties are immutable.
        // What we did here is what ORMs like EF Core do, so it's nothing new.
        playerInfo.SetValue(value: reader.GetInt32("id"),                 propertyName: nameof(PlayerInfo.AccountId));
        playerInfo.SetValue(value: reader.GetInt32("brought_flags"),      propertyName: nameof(PlayerInfo.BroughtFlags));
        playerInfo.SetValue(value: reader.GetInt32("captured_flags"),     propertyName: nameof(PlayerInfo.CapturedFlags));
        playerInfo.SetValue(value: reader.GetInt32("dropped_flags"),      propertyName: nameof(PlayerInfo.DroppedFlags));
        playerInfo.SetValue(value: reader.GetInt32("returned_flags"),     propertyName: nameof(PlayerInfo.ReturnedFlags));
        playerInfo.SetValue(value: reader.GetInt32("head_shots"),         propertyName: nameof(PlayerInfo.HeadShots));
        playerInfo.SetValue(value: reader.GetInt32("gungame_wins"),       propertyName: nameof(PlayerInfo.GunGameWins));
        playerInfo.SetValue(value: reader.GetDateTime("created_at"),      propertyName: nameof(PlayerInfo.CreatedAt));
        playerInfo.SetValue(value: reader.GetDateTime("last_connection"), propertyName: nameof(PlayerInfo.LastConnection));
        return playerInfo;
    }

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateBroughtFlags(PlayerInfo player)
        => Update(player.AccountId, "brought_flags", player.BroughtFlags);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateCapturedFlags(PlayerInfo player)
        => Update(player.AccountId, "captured_flags", player.CapturedFlags);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateDroppedFlags(PlayerInfo player)
        => Update(player.AccountId, "dropped_flags", player.DroppedFlags);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateReturnedFlags(PlayerInfo player)
        => Update(player.AccountId, "returned_flags", player.ReturnedFlags);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateHeadShots(PlayerInfo player)
        => Update(player.AccountId, "head_shots", player.HeadShots);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateGunGameWins(PlayerInfo player)
        => Update(player.AccountId, "gungame_wins", player.GunGameWins);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateLastConnection(PlayerInfo player)
        => Update(player.AccountId, "last_connection", player.LastConnection);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateMaxKillingSpree(PlayerInfo player)
        => Update(player.AccountId, "max_killing_spree", player.MaxKillingSpree);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateName(PlayerInfo player)
    {
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();
        connection.CreateRegexpFunction();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["UpdatePlayerName"];
        command.Parameters.AddWithValue("$id", player.AccountId);
        command.Parameters.AddWithValue("$name", player.Name);
        command.ExecuteNonQuery();
    }

    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdatePassword(PlayerInfo player)
    {
        var passwordHash = passwordHasher.HashPassword(player.Password);
        Update(player.AccountId, "password", passwordHash);
    }

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateRank(PlayerInfo player)
        => Update(player.AccountId, "rank_id", player.RankId);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateRole(PlayerInfo player)
        => Update(player.AccountId, "role_id", player.RoleId);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateSkin(PlayerInfo player)
        => Update(player.AccountId, "skin_id", player.SkinId);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateTotalDeaths(PlayerInfo player)
        => Update(player.AccountId, "total_deaths", player.TotalDeaths);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    public void UpdateTotalKills(PlayerInfo player) 
        => Update(player.AccountId, "total_kills", player.TotalKills);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-30 (SQLite SQL dialect)</remarks>
    private void Update(int id, string columnName, object value) 
    {
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = $"UPDATE players SET {columnName} = $column_value WHERE id = $id";
        command.Parameters.AddWithValue("$id", id);
        command.Parameters.AddWithValue("$column_value", value);
        command.ExecuteNonQuery();
    }
}
