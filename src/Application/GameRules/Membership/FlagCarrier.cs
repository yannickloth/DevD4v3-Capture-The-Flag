namespace CTF.Application.GameRules.Membership;

/// <summary>
/// Represents the player currently carrying a flag.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public class FlagCarrier
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    public Player Player { get; internal set; }

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state); CD-31 (player entity) → CD-02</remarks>
    public FlagCarrier(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        Player = player;
    }

    /// <summary>
    /// Determines whether the specified player is the carrier, matched by player name.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    public bool Is(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        return Player.Name.Equals(
            player.Name,
            StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets the display name of the carrier.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    public string DisplayName => Player.Name;
}
