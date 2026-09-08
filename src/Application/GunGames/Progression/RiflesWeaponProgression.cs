namespace CTF.Application.GunGames;

/// <summary>
/// Defines a GunGame weapon progression using only rifles.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class RiflesWeaponProgression : WeaponProgression
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override WeaponProgressionType Type => WeaponProgressionType.Rifles;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
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
