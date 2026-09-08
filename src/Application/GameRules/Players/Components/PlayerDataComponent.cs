namespace CTF.Application.GameRules.EcsNsNs3;

/// <summary>
/// Stores the runtime state required for player pause detection.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs)]
public class PlayerDataComponent : Component
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    private readonly Player _player;

    /// <summary>
    /// Gets the player's current state.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    public PlayerState State => _player.State;

    /// <summary>
    /// Gets or sets whether the player is currently paused.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    public bool IsPaused { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last received
    /// <c>OnPlayerUpdate</c> callback.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    public long LastUpdateTick { get; set; }

    /// <summary>Creates the component for the given player.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: pause-state component); CD-31 (player entity) → CD-02</remarks>
    public PlayerDataComponent(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        _player = player;
    }
}
