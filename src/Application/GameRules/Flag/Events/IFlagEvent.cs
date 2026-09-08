namespace CTF.Application.GameRules.CompositionDomain;

/// <summary>
/// Represents an event related to the flag in the game.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Composition)]
public interface IFlagEvent
{
    /// <summary>
    /// Gets the current status of the flag associated with the event.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    FlagStatus FlagStatus { get; }

    /// <summary>
    /// Handles the event when the flag is involved, updating the game state accordingly.
    /// </summary>
    /// <param name="team">The team associated with the event.</param>
    /// <param name="player">The player who triggered the event.</param>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    void Handle(Team team, Player player);
}
