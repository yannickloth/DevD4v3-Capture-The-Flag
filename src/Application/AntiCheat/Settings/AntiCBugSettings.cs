namespace CTF.Application.AntiCheat.Settings;

/// <summary>
/// Represents the configuration for the GTA: San Andreas crouch bug (C-Bug) protection.
/// </summary>
/// <remarks>
/// C-Bug is a bug in GTA: San Andreas that allows players to manipulate the
/// reload animation of certain weapons, particularly the Desert Eagle, to fire
/// much faster than the game's normal mechanics would allow.
/// </remarks>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Configuration)]
public class AntiCBugSettings
{
    /// <summary>Gets or sets a value indicating whether the C-Bug protection is disabled.</summary>
    [ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Configuration)]
    public bool Disabled { get; set; } = false;
}
