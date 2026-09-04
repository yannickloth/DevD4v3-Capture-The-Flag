namespace CTF.Application.GunGames;

/// <summary>
/// Defines a GunGame weapon progression using only submachine guns.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public class SmgsWeaponProgression : WeaponProgression
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override WeaponProgressionType Type => WeaponProgressionType.SMGs;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Tec9,
            WeaponDefinitions.Uzi,
            WeaponDefinitions.MP5,
            WeaponDefinitions.Tec9,
            WeaponDefinitions.Uzi,
            WeaponDefinitions.MP5,
            WeaponDefinitions.Tec9,
            WeaponDefinitions.Uzi,
            WeaponDefinitions.MP5,
            WeaponDefinitions.Knife
        ]);
    }
}
