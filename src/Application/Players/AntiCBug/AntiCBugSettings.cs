namespace CTF.Application.Players.AntiCBug;

/// <summary>
/// Represents the configuration for the GTA: San Andreas crouch bug (C-Bug) protection.
/// </summary>
/// <remarks>Change drivers: CD-17 (game configuration/.env schema), CD-14 (anti-cheat policy)</remarks>
/// <remarks>
/// C-Bug is a bug in GTA: San Andreas that allows players to manipulate the
/// reload animation of certain weapons, particularly the Desert Eagle, to fire
/// much faster than the game's normal mechanics would allow.
/// </remarks>
public class AntiCBugSettings
{
    /// <summary>Gets or sets a value indicating whether the C-Bug protection is disabled.</summary>
    /// <remarks>Change drivers: CD-17 (game configuration/.env schema), CD-14 (anti-cheat policy)</remarks>
    public bool Disabled { get; set; } = false;
}
