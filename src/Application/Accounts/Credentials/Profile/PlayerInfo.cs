namespace CTF.Application.Accounts.Credentials.Profile;

/// <summary>
/// Represents the account of a player as persisted in the database.
/// It composes the account's identity, career statistics, role, and appearance,
/// which are all stored in the same database row.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.Model)]
public partial class PlayerInfo
{
    [ChangeDriversAttribute(ChangeDriver.Account)]
    public PlayerAccount Account { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Account)]
    public PlayerStatistics Stats { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Coin)]
    public PlayerCoins Coins { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Account)]
    public PlayerRole Role { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Account)]
    public PlayerAppearance Appearance { get; } = new();
}
