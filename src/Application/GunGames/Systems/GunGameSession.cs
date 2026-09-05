namespace CTF.Application.GunGames.Systems;

/// <summary>
/// Represents the current GunGame session.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class GunGameSession
{
    /// <summary>
    /// Gets or sets the weapon progression used by the current GunGame session.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public WeaponProgressionType WeaponProgressionType { get; set; } = WeaponProgressionType.Classic;

    /// <summary>
    /// Gets or sets the number of kills required to advance to the next weapon level.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public KillsRequiredPerLevel KillsRequiredPerLevel { get; set; } = new(1);
}
