namespace CTF.Application.WeaponCatalogs;

[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
public class HeavyWeaponCatalog : WeaponCatalog
{
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    public override WeaponCatalogType Type => WeaponCatalogType.Heavy;

    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.RocketLauncher,
            WeaponDefinitions.Heatseeker,
            WeaponDefinitions.Minigun
        ]);
    }
}
