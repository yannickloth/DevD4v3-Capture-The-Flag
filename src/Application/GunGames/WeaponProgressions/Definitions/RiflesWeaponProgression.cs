namespace CTF.Application.GunGames.WeaponProgressions.Definitions;

/// <summary>
/// Defines a GunGame weapon progression using only rifles.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public class RiflesWeaponProgression : WeaponProgression
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override WeaponProgressionType Type => WeaponProgressionType.Rifles;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.Knife
        ]);
    }
}
