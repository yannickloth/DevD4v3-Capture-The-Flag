namespace CTF.Application.Players.Accounts;

/// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
public interface IPlayerRepository
{
    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    PlayerInfo GetOrDefault(string name);

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    bool Exists(string name);

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    void Create(PlayerInfo player);

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    void UpdateName(PlayerInfo player);

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08; CD-25 (BCrypt password-hashing contract) → CD-08</remarks>
    void UpdatePassword(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateTotalKills(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateTotalDeaths(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateMaxKillingSpree(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateBroughtFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateCapturedFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateDroppedFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateReturnedFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateHeadShots(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-07 (GunGame mode rules) → CD-10; CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateGunGameWins(PlayerInfo player);

    /// <remarks>Change drivers: CD-09 (root; authorization policy); CD-20 (outbound repository contract) → CD-09</remarks>
    void UpdateRole(PlayerInfo player);

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API); CD-20 (outbound repository contract) → CD-01</remarks>
    void UpdateSkin(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateRank(PlayerInfo player);

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    void UpdateLastConnection(PlayerInfo player);
}
