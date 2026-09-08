namespace CTF.Application.GunGames;

/// <summary>
/// Provides the context associated with a kill processed by <see cref="GunGame"/>.
/// </summary>
/// <param name="Victim">
/// The player who was killed.
/// </param>
/// <param name="Killer">
/// The player who performed the kill.
/// </param>
/// <param name="Reason">
/// The weapon used to perform the kill.
/// </param>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public readonly record struct KillContext(
    Player Victim, 
    Player Killer, 
    Weapon Reason
);

/// <summary>
/// Defines how a specific <see cref="GunGame"/> result should be handled.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public interface IGunGameResultHandler
{
    /// <summary>
    /// Gets the GunGame result associated with this handler.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    GunGameResult Result { get; }

    /// <summary>
    /// Handles the result produced by <see cref="GunGame"/>.
    /// </summary>
    /// <param name="context">
    /// The context of the processed kill.
    /// </param>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    void Handle(KillContext context);
}
