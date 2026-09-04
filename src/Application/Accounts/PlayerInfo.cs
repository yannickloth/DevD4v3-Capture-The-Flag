namespace CTF.Application.Accounts;

/// <summary>
/// Represents the account of a player as persisted in the database.
/// It composes the account's identity, career statistics, role, and appearance,
/// which are all stored in the same database row.
/// </summary>
/// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: player data model composition)</remarks>
public partial class PlayerInfo
{
    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: account identity/credentials)</remarks>
    public PlayerAccount Account { get; } = new();

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: career statistics composition); CD-10 (player-statistics/rank model) → CD-08</remarks>
    public PlayerStatistics Stats { get; } = new();

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: authorization composition); CD-09 (authorization policy) → CD-08</remarks>
    public PlayerRole Role { get; } = new();

    /// <remarks>Change drivers: CD-08 (root; account &amp; authentication policy: appearance composition); CD-44 (model &amp; skin id resources) → CD-08</remarks>
    public PlayerAppearance Appearance { get; } = new();
}
