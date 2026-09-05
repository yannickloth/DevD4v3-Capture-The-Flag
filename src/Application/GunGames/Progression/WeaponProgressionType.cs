namespace CTF.Application.GunGames.Progression;

/// <summary>
/// Identifies the available weapon progression types.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public enum WeaponProgressionType
{
    [DisplayName("Classic")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    Classic,

    [DisplayName("Reverse Classic")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    ReverseClassic,

    [DisplayName("Pistols Only")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    Pistols,

    [DisplayName("SMGs Only")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    SMGs,

    [DisplayName("Shotguns Only")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    Shotguns,

    [DisplayName("Rifles Only")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    Rifles,

    [DisplayName("Hardcore")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    Hardcore,

    [DisplayName("Powerful Weapons")]
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    Powerful
}
