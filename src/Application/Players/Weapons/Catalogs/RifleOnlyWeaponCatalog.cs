namespace CTF.Application.Players.Weapons.Catalogs;

/// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
public class RifleOnlyWeaponCatalog : WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    public override WeaponCatalogType Type => WeaponCatalogType.RifleOnly;

    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle
        ]);
    }
}
