namespace CTF.Application.GunGames.WeaponProgressions.Definitions;

/// <summary>
/// Defines the classic GunGame weapon progression,
/// where players advance through increasingly challenging
/// weapons until reaching the final knife level.
/// </summary>
/// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
public class ClassicWeaponProgression : WeaponProgression
{
    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    public override WeaponProgressionType Type => WeaponProgressionType.Classic;

    /// <remarks>Change drivers: CD-07 (GunGame mode rules)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        // Ordered from the first weapon level to the final weapon level.
        weapons.AddRange(
        [
            WeaponDefinitions.Silenced,
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.Tec9,
            WeaponDefinitions.Uzi,
            WeaponDefinitions.MP5,
            WeaponDefinitions.AK47,
            WeaponDefinitions.M4,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.Knife
        ]);
    }
}
