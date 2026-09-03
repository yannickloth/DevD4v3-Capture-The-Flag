namespace Persistence.InMemory;

/// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-25 (BCrypt password-hashing contract) → CD-20</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): players (Dictionary&lt;int, FakePlayer&gt;) -> CD-18; passwordHasher -> CD-25. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
internal class FakePlayerRepository(
    Dictionary<int, FakePlayer> players,
    IPasswordHasher passwordHasher) : IPlayerRepository
{
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-25 (BCrypt password-hashing contract) → CD-20</remarks>
    public void Create(PlayerInfo player)
    {
        var passwordHash = passwordHasher.HashPassword(player.Account.Password);
        var fakePlayer = new FakePlayer(player.Account.Name, passwordHash)
        {
            TotalKills       = player.Stats.TotalKills,
            TotalDeaths      = player.Stats.TotalDeaths,
            MaxKillingSpree  = player.Stats.MaxKillingSpree,
            BroughtFlags     = player.Stats.BroughtFlags,
            CapturedFlags    = player.Stats.CapturedFlags,
            DroppedFlags     = player.Stats.DroppedFlags,
            ReturnedFlags    = player.Stats.ReturnedFlags,
            HeadShots        = player.Stats.HeadShots,
            GunGameWins      = player.Stats.GunGameWins,
            SkinId           = player.Appearance.SkinId,
            RoleId           = player.Role.Id,
            RankId           = player.Stats.RankId,
            CreatedAt        = player.Account.CreatedAt,
            LastConnection   = player.Stats.LastConnection
        };
        players.Add(fakePlayer.Id, fakePlayer);
        // The Account ID is immutable and lacks a public setter; Reflection is used to modify it.
        player.Account.SetValue(value: fakePlayer.Id, propertyName: nameof(PlayerAccount.AccountId));
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public bool Exists(string name)
        => players.Any(player => player.Value.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
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
        playerInfo.Account.SetValue(value: fakePlayer.PasswordHash, propertyName: nameof(PlayerAccount.Password));

        playerInfo.Account.SetName(fakePlayer.Name);
        playerInfo.Stats.SetTotalKills(fakePlayer.TotalKills);
        playerInfo.Stats.SetTotalDeaths(fakePlayer.TotalDeaths);
        playerInfo.Stats.SetMaxKillingSpree(fakePlayer.MaxKillingSpree);
        playerInfo.Role.Set(fakePlayer.RoleId);
        playerInfo.Stats.SetRank(fakePlayer.RankId);
        playerInfo.Appearance.SetSkin(fakePlayer.SkinId);

        // Reflection is used here because these properties are immutable.
        // What we did here is what ORMs like EF Core do, so it's nothing new.
        playerInfo.Account.SetValue(value: fakePlayer.Id,             propertyName: nameof(PlayerAccount.AccountId));
        playerInfo.Stats.SetValue(value: fakePlayer.BroughtFlags,     propertyName: nameof(PlayerStatistics.BroughtFlags));
        playerInfo.Stats.SetValue(value: fakePlayer.CapturedFlags,    propertyName: nameof(PlayerStatistics.CapturedFlags));
        playerInfo.Stats.SetValue(value: fakePlayer.DroppedFlags,     propertyName: nameof(PlayerStatistics.DroppedFlags));
        playerInfo.Stats.SetValue(value: fakePlayer.ReturnedFlags,    propertyName: nameof(PlayerStatistics.ReturnedFlags));
        playerInfo.Stats.SetValue(value: fakePlayer.HeadShots,        propertyName: nameof(PlayerStatistics.HeadShots));
        playerInfo.Stats.SetValue(value: fakePlayer.GunGameWins,      propertyName: nameof(PlayerStatistics.GunGameWins));
        playerInfo.Account.SetValue(value: fakePlayer.CreatedAt,      propertyName: nameof(PlayerAccount.CreatedAt));
        playerInfo.Stats.SetValue(value: fakePlayer.LastConnection,   propertyName: nameof(PlayerStatistics.LastConnection));
        return playerInfo;
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateBroughtFlags(PlayerInfo player) 
        => players[player.Account.AccountId].BroughtFlags = player.Stats.BroughtFlags;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateCapturedFlags(PlayerInfo player)
        => players[player.Account.AccountId].CapturedFlags = player.Stats.CapturedFlags;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateDroppedFlags(PlayerInfo player)
        => players[player.Account.AccountId].DroppedFlags = player.Stats.DroppedFlags;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateReturnedFlags(PlayerInfo player)
        => players[player.Account.AccountId].ReturnedFlags = player.Stats.ReturnedFlags;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateHeadShots(PlayerInfo player)
        => players[player.Account.AccountId].HeadShots = player.Stats.HeadShots;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateGunGameWins(PlayerInfo player)
        => players[player.Account.AccountId].GunGameWins = player.Stats.GunGameWins;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateLastConnection(PlayerInfo player)
        => players[player.Account.AccountId].LastConnection = player.Stats.LastConnection;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateMaxKillingSpree(PlayerInfo player)
        => players[player.Account.AccountId].MaxKillingSpree = player.Stats.MaxKillingSpree;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateName(PlayerInfo player)
        => players[player.Account.AccountId].Name = player.Account.Name;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-25 (BCrypt password-hashing contract) → CD-20</remarks>
    public void UpdatePassword(PlayerInfo player)
       => players[player.Account.AccountId].PasswordHash = passwordHasher.HashPassword(player.Account.Password);

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateRank(PlayerInfo player)
        => players[player.Account.AccountId].RankId = player.Stats.RankId;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateRole(PlayerInfo player)
        => players[player.Account.AccountId].RoleId = player.Role.Id;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateSkin(PlayerInfo player)
        => players[player.Account.AccountId].SkinId = player.Appearance.SkinId;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateTotalDeaths(PlayerInfo player)
        => players[player.Account.AccountId].TotalDeaths = player.Stats.TotalDeaths;

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20</remarks>
    public void UpdateTotalKills(PlayerInfo player)
        => players[player.Account.AccountId].TotalKills = player.Stats.TotalKills;
}
