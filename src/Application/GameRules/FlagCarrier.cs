namespace CTF.Application.GameRules;

/// <summary>
/// Represents the player currently carrying a flag.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state); CD-01 (open.mp/SampSharp platform API: player entity) → CD-02</remarks>
public class FlagCarrier
{
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: one-flag-per-player carrier rule); CD-01 (open.mp/SampSharp platform API: player entity) → CD-02</remarks>
    public Player Player { get; internal set; }

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state)</remarks>
    public FlagCarrier(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        Player = player;
    }

    /// <summary>
    /// Determines whether the specified player is the carrier, matched by player name.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag carrier state, carrier matching by nickname); CD-01 (open.mp/SampSharp platform API: player entity/name) → CD-02</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier display); CD-01 (open.mp/SampSharp platform API: player entity/name) → CD-02</remarks>
    public string DisplayName => Player.Name;
}
