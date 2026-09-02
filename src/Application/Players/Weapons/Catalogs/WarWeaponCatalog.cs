namespace CTF.Application.Players.Weapons.Catalogs;

/// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
public class WarWeaponCatalog : WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public override WeaponCatalogType Type => WeaponCatalogType.War;

    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
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
