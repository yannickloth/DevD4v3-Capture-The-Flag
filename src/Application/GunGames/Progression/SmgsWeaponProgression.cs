namespace CTF.Application.GunGames.Progression;

/// <summary>
/// Defines a GunGame weapon progression using only submachine guns.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class SmgsWeaponProgression : WeaponProgression
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override WeaponProgressionType Type => WeaponProgressionType.SMGs;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
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
