namespace Persistence.InMemory;

/// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-21 (DI container/composition)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): players (Dictionary&lt;int, FakePlayer&gt;) -> CD-18; passwordHasher -> CD-25. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
internal class FakePlayerRepository(
    Dictionary<int, FakePlayer> players,
    IPasswordHasher passwordHasher) : IPlayerRepository
{
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void Create(PlayerInfo player)
    {
        var passwordHash = passwordHasher.HashPassword(player.Password);
        var fakePlayer = new FakePlayer(player.Name, passwordHash)
        {
            TotalKills       = player.TotalKills,
            TotalDeaths      = player.TotalDeaths,
            MaxKillingSpree  = player.MaxKillingSpree,
            BroughtFlags     = player.BroughtFlags,
            CapturedFlags    = player.CapturedFlags,
            DroppedFlags     = player.DroppedFlags,
            ReturnedFlags    = player.ReturnedFlags,
            HeadShots        = player.HeadShots,
            GunGameWins      = player.GunGameWins,
            SkinId           = player.SkinId,
            RoleId           = player.RoleId,
            RankId           = player.RankId,
            CreatedAt        = player.CreatedAt,
            LastConnection   = player.LastConnection
        };
        players.Add(fakePlayer.Id, fakePlayer);
        // The Account ID is immutable and lacks a public setter; Reflection is used to modify it.
        player.SetValue(value: fakePlayer.Id, propertyName: nameof(PlayerInfo.AccountId));
    }

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public bool Exists(string name)
        => players.Any(player => player.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public PlayerInfo GetOrDefault(string name)
    {
        FakePlayer fakePlayer = players
            .Where(player => player.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            .Select(player => player.Value)
            .FirstOrDefault();

        if (fakePlayer is null)
            return default;

        var playerInfo = new PlayerInfo();
        // The public setter is used only for plaintext passwords.
        // For that reason, we use Reflection to set the already encrypted password.
        playerInfo.SetValue(value: fakePlayer.PasswordHash, propertyName: nameof(PlayerInfo.Password));

        playerInfo.SetName(fakePlayer.Name);
        playerInfo.SetTotalKills(fakePlayer.TotalKills);
        playerInfo.SetTotalDeaths(fakePlayer.TotalDeaths);
        playerInfo.SetMaxKillingSpree(fakePlayer.MaxKillingSpree);
        playerInfo.SetRole(fakePlayer.RoleId);
        playerInfo.SetRank(fakePlayer.RankId);
        playerInfo.SetSkin(fakePlayer.SkinId);

        // Reflection is used here because these properties are immutable.
        // What we did here is what ORMs like EF Core do, so it's nothing new.
        playerInfo.SetValue(value: fakePlayer.Id,             propertyName: nameof(PlayerInfo.AccountId));
        playerInfo.SetValue(value: fakePlayer.BroughtFlags,   propertyName: nameof(PlayerInfo.BroughtFlags));
        playerInfo.SetValue(value: fakePlayer.CapturedFlags,  propertyName: nameof(PlayerInfo.CapturedFlags));
        playerInfo.SetValue(value: fakePlayer.DroppedFlags,   propertyName: nameof(PlayerInfo.DroppedFlags));
        playerInfo.SetValue(value: fakePlayer.ReturnedFlags,  propertyName: nameof(PlayerInfo.ReturnedFlags));
        playerInfo.SetValue(value: fakePlayer.HeadShots,      propertyName: nameof(PlayerInfo.HeadShots));
        playerInfo.SetValue(value: fakePlayer.GunGameWins,    propertyName: nameof(PlayerInfo.GunGameWins));
        playerInfo.SetValue(value: fakePlayer.CreatedAt,      propertyName: nameof(PlayerInfo.CreatedAt));
        playerInfo.SetValue(value: fakePlayer.LastConnection, propertyName: nameof(PlayerInfo.LastConnection));
        return playerInfo;
    }

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateBroughtFlags(PlayerInfo player) 
        => players[player.AccountId].BroughtFlags = player.BroughtFlags;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateCapturedFlags(PlayerInfo player)
        => players[player.AccountId].CapturedFlags = player.CapturedFlags;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateDroppedFlags(PlayerInfo player)
        => players[player.AccountId].DroppedFlags = player.DroppedFlags;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateReturnedFlags(PlayerInfo player)
        => players[player.AccountId].ReturnedFlags = player.ReturnedFlags;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateHeadShots(PlayerInfo player)
        => players[player.AccountId].HeadShots = player.HeadShots;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateGunGameWins(PlayerInfo player)
        => players[player.AccountId].GunGameWins = player.GunGameWins;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateLastConnection(PlayerInfo player)
        => players[player.AccountId].LastConnection = player.LastConnection;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateMaxKillingSpree(PlayerInfo player)
        => players[player.AccountId].MaxKillingSpree = player.MaxKillingSpree;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateName(PlayerInfo player)
        => players[player.AccountId].Name = player.Name;

    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract), CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdatePassword(PlayerInfo player)
       => players[player.AccountId].PasswordHash = passwordHasher.HashPassword(player.Password);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateRank(PlayerInfo player)
        => players[player.AccountId].RankId = player.RankId;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateRole(PlayerInfo player)
        => players[player.AccountId].RoleId = player.RoleId;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateSkin(PlayerInfo player)
        => players[player.AccountId].SkinId = player.SkinId;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateTotalDeaths(PlayerInfo player)
        => players[player.AccountId].TotalDeaths = player.TotalDeaths;

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public void UpdateTotalKills(PlayerInfo player)
        => players[player.AccountId].TotalKills = player.TotalKills;
}
