namespace CTF.Application.WeaponCatalogs;

[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
public class WarWeaponCatalog : WeaponCatalog
{
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    public override WeaponCatalogType Type => WeaponCatalogType.War;

    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Deagle,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.M4,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.Grenade,
            WeaponDefinitions.Molotov,
            WeaponDefinitions.TearGas,
            WeaponDefinitions.Flamethrower,
            WeaponDefinitions.SatchelCharge
        ]);
    }
}
