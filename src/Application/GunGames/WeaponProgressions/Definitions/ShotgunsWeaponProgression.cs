namespace CTF.Application.GunGames.WeaponProgressions.Definitions;

/// <summary>
/// Defines a GunGame weapon progression using only shotguns.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public class ShotgunsWeaponProgression : WeaponProgression
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override WeaponProgressionType Type => WeaponProgressionType.Shotguns;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.Knife
        ]);
    }
}
