namespace CTF.Application.GunGames;

/// <summary>
/// Defines a GunGame weapon progression using only pistols.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public class PistolsWeaponProgression : WeaponProgression
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override WeaponProgressionType Type => WeaponProgressionType.Pistols;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Silenced,
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.Silenced,
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.Silenced,
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.Knife
        ]);
    }
}
