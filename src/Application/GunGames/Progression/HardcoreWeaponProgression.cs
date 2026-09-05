namespace CTF.Application.GunGames.Progression;

/// <summary>
/// Defines a GunGame weapon progression using only high-skill weapons.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class HardcoreWeaponProgression : WeaponProgression
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override WeaponProgressionType Type => WeaponProgressionType.Hardcore;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
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
