namespace CTF.Application.GameRules;

/// <summary>
/// Stores the runtime state required for player pause detection.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-31 (player entity & state); CD-32 (Component) → CD-02</remarks>
public class PlayerDataComponent : Component
{
    /// <remarks>Change drivers: CD-31 (root; player entity reference)</remarks>
    private readonly Player _player;

    /// <summary>
    /// Gets the player's current state.
    /// </summary>
    /// <remarks>Change drivers: CD-31 (root; player entity)</remarks>
    public PlayerState State => _player.State;

    /// <summary>
    /// Gets or sets whether the player is currently paused.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification)</remarks>
    public bool IsPaused { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last received
    /// <c>OnPlayerUpdate</c> callback.
    /// </summary>
    /// <remarks>Change drivers: CD-31 (root; player entity)</remarks>
    public long LastUpdateTick { get; set; }

    /// <summary>Creates the component for the given player.</summary>
    /// <remarks>Change drivers: CD-31 (root; player entity)</remarks>
    public PlayerDataComponent(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        _player = player;
    }
}
