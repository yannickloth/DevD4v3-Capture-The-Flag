namespace CTF.Application.GunGames;

/// <summary>
/// Defines a GunGame weapon progression using only pistols.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class PistolsWeaponProgression : WeaponProgression
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override WeaponProgressionType Type => WeaponProgressionType.Pistols;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
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
