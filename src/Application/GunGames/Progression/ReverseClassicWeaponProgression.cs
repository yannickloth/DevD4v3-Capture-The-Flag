namespace CTF.Application.GunGames;

/// <summary>
/// Defines the reverse of the classic GunGame weapon progression.
/// Players begin with the most difficult weapons and finish with the easiest,
/// before reaching the final knife level.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class ReverseClassicWeaponProgression : WeaponProgression
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override WeaponProgressionType Type => WeaponProgressionType.ReverseClassic;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.SniperRifle,
            WeaponDefinitions.CountryRifle,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.M4,
            WeaponDefinitions.AK47,
            WeaponDefinitions.MP5,
            WeaponDefinitions.Uzi,
            WeaponDefinitions.Tec9,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Silenced,
            WeaponDefinitions.Knife
        ]);
    }
}
