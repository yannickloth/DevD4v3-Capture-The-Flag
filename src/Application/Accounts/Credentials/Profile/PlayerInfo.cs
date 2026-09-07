namespace CTF.Application.Accounts.Credentials.Profile;

/// <summary>
/// Represents the account of a player as persisted in the database.
/// It composes the account's identity, career statistics, role, and appearance,
/// which are all stored in the same database row.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Coin)]
public partial class PlayerInfo
{
    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Coin)]
    public PlayerAccount Account { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Coin)]
    public PlayerStatistics Stats { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Coin)]
    public PlayerCoins Coins { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Coin)]
    public PlayerRole Role { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Coin)]
    public PlayerAppearance Appearance { get; } = new();
}
