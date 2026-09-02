namespace CTF.Application.Players.Weapons.Catalogs;

/// <summary>
/// Defines a weapon catalog that combines the Walking and Run weapon catalogs.
/// </summary>
/// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
/// <remarks>
/// This catalog contains all weapons available from both categories.
/// </remarks>
public class MixedWeaponCatalog : WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public override WeaponCatalogType Type => WeaponCatalogType.Mixed;

    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
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
