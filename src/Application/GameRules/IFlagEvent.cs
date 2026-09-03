namespace CTF.Application.GameRules;

/// <summary>
/// Represents an event related to the flag in the game.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag events); CD-21 (DI container/composition: event wiring) → CD-02</remarks>
public interface IFlagEvent
{
    /// <summary>
    /// Gets the current status of the flag associated with the event.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag state machine)</remarks>
    FlagStatus FlagStatus { get; }

    /// <summary>
    /// Handles the event when the flag is involved, updating the game state accordingly.
    /// </summary>
    /// <param name="team">The team associated with the event.</param>
    /// <param name="player">The player who triggered the event.</param>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag events)</remarks>
    void Handle(Team team, Player player);
}
