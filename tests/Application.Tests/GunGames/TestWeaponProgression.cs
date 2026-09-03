namespace CTF.Application.Tests.GunGames;

/// <summary>Test double for WeaponProgression (fixture).</summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponProgression (fixture))</remarks>
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

/// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponProgression (fixture))</remarks>
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
