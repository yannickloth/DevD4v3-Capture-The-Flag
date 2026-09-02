namespace CTF.Application.GunGames;

/// <summary>
/// Represents the current availability of GunGame mode.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
public interface IGunGameMode
{
    /// <summary>
    /// Gets a value indicating whether GunGame mode is currently active.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    bool IsEnabled { get; }
}
