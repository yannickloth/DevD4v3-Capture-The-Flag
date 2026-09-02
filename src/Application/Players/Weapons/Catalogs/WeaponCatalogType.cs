namespace CTF.Application.Players.Weapons.Catalogs;

/// <summary>
/// Represents the available weapon catalogs.
/// </summary>
/// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
public enum WeaponCatalogType
{
    [DisplayName("Walking Weapons")]
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    Walking,

    [DisplayName("Run Weapons")]
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    Run,

    [DisplayName("Run & Walk Weapons")]
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    Mixed,

    [DisplayName("Rifles Only")]
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    RifleOnly,

    [DisplayName("War Weapons")]
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    War,

    [DisplayName("Heavy Weapons")]
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    Heavy,

    [DisplayName("Melee Weapons")]
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    Melee
}
