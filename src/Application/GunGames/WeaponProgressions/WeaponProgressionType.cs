namespace CTF.Application.GunGames.WeaponProgressions;

/// <summary>
/// Identifies the available weapon progression types.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
public enum WeaponProgressionType
{
    [DisplayName("Classic")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    Classic,

    [DisplayName("Reverse Classic")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    ReverseClassic,

    [DisplayName("Pistols Only")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    Pistols,

    [DisplayName("SMGs Only")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    SMGs,

    [DisplayName("Shotguns Only")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    Shotguns,

    [DisplayName("Rifles Only")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    Rifles,

    [DisplayName("Hardcore")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    Hardcore,

    [DisplayName("Powerful Weapons")]
    /// <remarks>Change drivers: CD-07 (root; root; GunGame mode rules)</remarks>
    Powerful
}
