namespace CTF.Application.Tests.Players.Weapons;

/// <summary>Test double for WeaponCatalog (fixture).</summary>
/// <remarks>Change drivers: CD-29 (root; code-under-test: WeaponCatalog (fixture); CD-04 (weapon-catalog configuration) → CD-29</remarks>
public class TestWeaponCatalog : WeaponCatalog
{
    public override WeaponCatalogType Type => WeaponCatalogType.Mixed;

    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.Tec9,
            WeaponDefinitions.Deagle,
            WeaponDefinitions.AK47,
            WeaponDefinitions.CombatShotgun
        ]);
    }
}
