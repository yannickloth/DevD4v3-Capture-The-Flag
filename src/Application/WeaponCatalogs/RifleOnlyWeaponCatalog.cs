namespace CTF.Application.WeaponCatalogs;

[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
public class RifleOnlyWeaponCatalog : WeaponCatalog
{
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    public override WeaponCatalogType Type => WeaponCatalogType.RifleOnly;

    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle
        ]);
    }
}
