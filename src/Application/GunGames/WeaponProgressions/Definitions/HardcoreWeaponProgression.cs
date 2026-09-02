namespace CTF.Application.GunGames.WeaponProgressions.Definitions;

/// <summary>
/// Defines a GunGame weapon progression using only high-skill weapons.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public class HardcoreWeaponProgression : WeaponProgression
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override WeaponProgressionType Type => WeaponProgressionType.Hardcore;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Deagle,
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.Knife
        ]);
    }
}
