namespace CTF.Application.Players.Weapons.Catalogs;

/// <summary>
/// Defines a weapon catalog that restricts player mobility while fighting.
/// </summary>
/// <remarks>Change drivers: CD-04 (root; root; weapon-catalog configuration)</remarks>
/// <remarks>
/// Players can only walk while using most weapons in this catalog.
/// </remarks>
public class WalkingWeaponCatalog : WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (root; root; weapon-catalog configuration)</remarks>
    public override WeaponCatalogType Type => WeaponCatalogType.Walking;

    /// <remarks>Change drivers: CD-04 (root; root; weapon-catalog configuration)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Silenced,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.MP5,
            WeaponDefinitions.AK47,
            WeaponDefinitions.M4,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle
        ]);
    }
}
