namespace CTF.Application.Accounts;

[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository)]
public interface IPlayerRepository
{
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository)]
    PlayerInfo GetOrDefault(string name);

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository)]
    bool Exists(string name);

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository)]
    void Create(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository)]
    void UpdateName(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.BCrypt)]
    void UpdatePassword(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateTotalKills(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateTotalDeaths(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateMaxKillingSpree(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateBroughtFlags(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateCapturedFlags(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateDroppedFlags(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateReturnedFlags(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateHeadShots(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.GunGame, ChangeDriver.Repository)]
    void UpdateGunGameWins(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository)]
    void UpdateRole(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Repository)]
    void UpdateSkin(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateRank(PlayerInfo player);

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    void UpdateLastConnection(PlayerInfo player);
}
