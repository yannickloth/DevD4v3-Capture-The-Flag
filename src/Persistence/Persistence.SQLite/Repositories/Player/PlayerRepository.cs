namespace Persistence.SQLite.Repositories.Player;

/// <remarks>Injected dependencies (change drivers of these elements): passwordHasher -> CD-25; sqlCollection -> CD-18; settings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
internal class PlayerRepository(
    IPasswordHasher passwordHasher,
    ISqlCollection sqlCollection,
    SQLiteSettings settings) : IPlayerRepository
{
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void Create(PlayerInfo player)
    {
        var passwordHash = passwordHasher.HashPassword(player.Account.Password);
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();
        connection.CreateRegexpFunction();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["CreatePlayer"];
        command.Parameters.AddWithValue("$name",              player.Account.Name);
        command.Parameters.AddWithValue("$password",          passwordHash);
        command.Parameters.AddWithValue("$total_kills",       player.Stats.TotalKills);
        command.Parameters.AddWithValue("$total_deaths",      player.Stats.TotalDeaths);
        command.Parameters.AddWithValue("$max_killing_spree", player.Stats.MaxKillingSpree);
        command.Parameters.AddWithValue("$brought_flags",     player.Stats.BroughtFlags);
        command.Parameters.AddWithValue("$captured_flags",    player.Stats.CapturedFlags);
        command.Parameters.AddWithValue("$dropped_flags",     player.Stats.DroppedFlags);
        command.Parameters.AddWithValue("$returned_flags",    player.Stats.ReturnedFlags);
        command.Parameters.AddWithValue("$head_shots",        player.Stats.HeadShots);
        command.Parameters.AddWithValue("$gungame_wins",      player.Stats.GunGameWins);
        command.Parameters.AddWithValue("$role_id",           player.Role.Id);
        command.Parameters.AddWithValue("$skin_id",           player.Appearance.SkinId);
        command.Parameters.AddWithValue("$rank_id",           player.Stats.RankId);
        command.Parameters.AddWithValue("$created_at",        player.Account.CreatedAt);
        command.Parameters.AddWithValue("$last_connection",   player.Stats.LastConnection);
        int id = (int)(long)command.ExecuteScalar();

        // The Account ID is immutable and lacks a public setter; Reflection is used to modify it.
        player.Account.SetValue(value: id, propertyName: nameof(PlayerAccount.AccountId));
    }

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
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

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
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
        playerInfo.Account.SetValue(value: reader.GetString("password"), propertyName: nameof(PlayerAccount.Password));

        playerInfo.Account.SetName(reader.GetString("name"));
        playerInfo.Stats.SetTotalKills(reader.GetInt32("total_kills"));
        playerInfo.Stats.SetTotalDeaths(reader.GetInt32("total_deaths"));
        playerInfo.Stats.SetMaxKillingSpree(reader.GetInt32("max_killing_spree"));
        playerInfo.Role.Set((RoleId)reader.GetInt32("role_id"));
        playerInfo.Stats.SetRank((RankId)reader.GetInt32("rank_id"));
        playerInfo.Appearance.SetSkin(reader.GetInt32("skin_id"));

        // Reflection is used here because these properties are immutable.
        // What we did here is what ORMs like EF Core do, so it's nothing new.
        playerInfo.Account.SetValue(value: reader.GetInt32("id"),                 propertyName: nameof(PlayerAccount.AccountId));
        playerInfo.Stats.SetValue(value: reader.GetInt32("brought_flags"),        propertyName: nameof(PlayerStatistics.BroughtFlags));
        playerInfo.Stats.SetValue(value: reader.GetInt32("captured_flags"),       propertyName: nameof(PlayerStatistics.CapturedFlags));
        playerInfo.Stats.SetValue(value: reader.GetInt32("dropped_flags"),        propertyName: nameof(PlayerStatistics.DroppedFlags));
        playerInfo.Stats.SetValue(value: reader.GetInt32("returned_flags"),       propertyName: nameof(PlayerStatistics.ReturnedFlags));
        playerInfo.Stats.SetValue(value: reader.GetInt32("head_shots"),           propertyName: nameof(PlayerStatistics.HeadShots));
        playerInfo.Stats.SetValue(value: reader.GetInt32("gungame_wins"),         propertyName: nameof(PlayerStatistics.GunGameWins));
        playerInfo.Account.SetValue(value: reader.GetDateTime("created_at"),      propertyName: nameof(PlayerAccount.CreatedAt));
        playerInfo.Stats.SetValue(value: reader.GetDateTime("last_connection"),   propertyName: nameof(PlayerStatistics.LastConnection));
        return playerInfo;
    }

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateBroughtFlags(PlayerInfo player)
        => Update(player.Account.AccountId, "brought_flags", player.Stats.BroughtFlags);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateCapturedFlags(PlayerInfo player)
        => Update(player.Account.AccountId, "captured_flags", player.Stats.CapturedFlags);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateDroppedFlags(PlayerInfo player)
        => Update(player.Account.AccountId, "dropped_flags", player.Stats.DroppedFlags);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateReturnedFlags(PlayerInfo player)
        => Update(player.Account.AccountId, "returned_flags", player.Stats.ReturnedFlags);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateHeadShots(PlayerInfo player)
        => Update(player.Account.AccountId, "head_shots", player.Stats.HeadShots);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateGunGameWins(PlayerInfo player)
        => Update(player.Account.AccountId, "gungame_wins", player.Stats.GunGameWins);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateLastConnection(PlayerInfo player)
        => Update(player.Account.AccountId, "last_connection", player.Stats.LastConnection);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateMaxKillingSpree(PlayerInfo player)
        => Update(player.Account.AccountId, "max_killing_spree", player.Stats.MaxKillingSpree);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateName(PlayerInfo player)
    {
        using var connection = new SqliteConnection(settings.ConnectionString);
        connection.Open();
        connection.CreateRegexpFunction();

        SqliteCommand command = connection.CreateCommand();
        command.CommandText = sqlCollection["UpdatePlayerName"];
        command.Parameters.AddWithValue("$id", player.Account.AccountId);
        command.Parameters.AddWithValue("$name", player.Account.Name);
        command.ExecuteNonQuery();
    }

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdatePassword(PlayerInfo player)
    {
        var passwordHash = passwordHasher.HashPassword(player.Account.Password);
        Update(player.Account.AccountId, "password", passwordHash);
    }

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateRank(PlayerInfo player)
        => Update(player.Account.AccountId, "rank_id", player.Stats.RankId);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateRole(PlayerInfo player)
        => Update(player.Account.AccountId, "role_id", player.Role.Id);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateSkin(PlayerInfo player)
        => Update(player.Account.AccountId, "skin_id", player.Appearance.SkinId);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateTotalDeaths(PlayerInfo player)
        => Update(player.Account.AccountId, "total_deaths", player.Stats.TotalDeaths);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
    public void UpdateTotalKills(PlayerInfo player) 
        => Update(player.Account.AccountId, "total_kills", player.Stats.TotalKills);

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
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
