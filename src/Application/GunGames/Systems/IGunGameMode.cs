namespace CTF.Application.GunGames.Systems;

/// <summary>
/// Represents the current availability of GunGame mode.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public interface IGunGameMode
{
    /// <summary>
    /// Gets a value indicating whether GunGame mode is currently active.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    bool IsEnabled { get; }
}
