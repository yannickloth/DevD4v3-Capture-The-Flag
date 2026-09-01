namespace CTF.Application.Players.Weapons.Catalogs;

/// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
public class MeleeWeaponCatalog : WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    public override WeaponCatalogType Type => WeaponCatalogType.Melee;

    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Chainsaw,
            WeaponDefinitions.GolfClub,
            WeaponDefinitions.Nitestick,
            WeaponDefinitions.BaseballBat,
            WeaponDefinitions.Shovel,
            WeaponDefinitions.Poolstick,
            WeaponDefinitions.Katana,
            WeaponDefinitions.Dildo,
            WeaponDefinitions.PurpleDildo,
            WeaponDefinitions.Vibrator,
            WeaponDefinitions.SilverVibrator,
            WeaponDefinitions.Cane,
            WeaponDefinitions.Flower,
            WeaponDefinitions.Spraycan,
            WeaponDefinitions.FireExtinguisher
        ]);
    }
}
