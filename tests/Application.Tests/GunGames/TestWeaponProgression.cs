namespace CTF.Application.GunGames;

/// <summary>Test double for WeaponProgression (fixture).</summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class TestWeaponProgression : WeaponProgression
{
    public override WeaponProgressionType Type => WeaponProgressionType.Classic;

    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Shotgun,
            WeaponDefinitions.AK47,
            WeaponDefinitions.Knife
        ]);
    }
}

[ChangeDriversAttribute(ChangeDriver.GunGame)]
public class NonKnifeFinalWeaponProgression : WeaponProgression
{
    public override WeaponProgressionType Type => WeaponProgressionType.Classic;

    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Colt45,
            WeaponDefinitions.MP5,
            WeaponDefinitions.Knife,
            WeaponDefinitions.Minigun
        ]);
    }
}
