namespace CTF.Application.WeaponCatalogs;

/// <summary>
/// Defines a weapon catalog that combines the Walking and Run weapon catalogs.
/// </summary>
/// <remarks>
/// This catalog contains all weapons available from both categories.
/// </remarks>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
public class MixedWeaponCatalog : WeaponCatalog
{
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    public override WeaponCatalogType Type => WeaponCatalogType.Mixed;

    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Silenced,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.MP5,
            WeaponDefinitions.Uzi,
            WeaponDefinitions.Tec9,
            WeaponDefinitions.AK47,
            WeaponDefinitions.M4,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle
        ]);
    }
}
