namespace CTF.Application.WeaponCatalogs;

/// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
public class HeavyWeaponCatalog : WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public override WeaponCatalogType Type => WeaponCatalogType.Heavy;

    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
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
