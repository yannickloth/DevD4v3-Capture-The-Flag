namespace CTF.Application.WeaponCatalogs;

/// <summary>
/// Represents the available weapon catalogs.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
public enum WeaponCatalogType
{
    [DisplayName("Walking Weapons")]
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    Walking,

    [DisplayName("Run Weapons")]
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    Run,

    [DisplayName("Run & Walk Weapons")]
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    Mixed,

    [DisplayName("Rifles Only")]
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    RifleOnly,

    [DisplayName("War Weapons")]
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    War,

    [DisplayName("Heavy Weapons")]
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    Heavy,

    [DisplayName("Melee Weapons")]
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    Melee
}
