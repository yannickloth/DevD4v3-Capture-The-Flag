namespace CTF.Application.Players.Accounts;

/// <remarks>Change drivers: CD-20 (outbound repository contract), CD-08 (account & authentication policy)</remarks>
public interface IPlayerRepository
{
    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-08 (account & authentication policy)</remarks>
    PlayerInfo GetOrDefault(string name);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-08 (account & authentication policy)</remarks>
    bool Exists(string name);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-08 (account & authentication policy)</remarks>
    void Create(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-08 (account & authentication policy)</remarks>
    void UpdateName(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-08 (account & authentication policy), CD-25 (BCrypt password-hashing contract)</remarks>
    void UpdatePassword(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateTotalKills(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateTotalDeaths(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateMaxKillingSpree(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateBroughtFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateCapturedFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateDroppedFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateReturnedFlags(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateHeadShots(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model), CD-07 (GunGame mode rules)</remarks>
    void UpdateGunGameWins(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-09 (authorization policy)</remarks>
    void UpdateRole(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-01 (open.mp/SampSharp platform API)</remarks>
    void UpdateSkin(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateRank(PlayerInfo player);

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-10 (player-statistics/rank model)</remarks>
    void UpdateLastConnection(PlayerInfo player);
}
