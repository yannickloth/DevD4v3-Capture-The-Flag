namespace CTF.Application.Tests.WeaponCatalogs.Catalogs;

/// <summary>Test double for WeaponCatalog (fixture).</summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
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
