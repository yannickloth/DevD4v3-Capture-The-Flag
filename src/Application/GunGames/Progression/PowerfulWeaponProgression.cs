namespace CTF.Application.GunGames;

/// <summary>
/// Defines a GunGame weapon progression featuring the most powerful weapons in GTA.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class PowerfulWeaponProgression : WeaponProgression
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override WeaponProgressionType Type => WeaponProgressionType.Powerful;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.MP5,
            WeaponDefinitions.AK47,
            WeaponDefinitions.M4,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.CombatShotgun,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.Flamethrower,
            WeaponDefinitions.RocketLauncher,
            WeaponDefinitions.Heatseeker,
            WeaponDefinitions.Minigun,
            WeaponDefinitions.Chainsaw,
            WeaponDefinitions.Grenade
        ]);
    }
}
