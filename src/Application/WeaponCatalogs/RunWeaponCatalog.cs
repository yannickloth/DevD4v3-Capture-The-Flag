namespace CTF.Application.WeaponCatalogs;

/// <summary>
/// Defines a weapon catalog that allows players to remain mobile while fighting.
/// </summary>
/// <remarks>
/// These weapons support the classic Run Weapons (RW) gameplay style,
/// where players can move quickly while attacking.
/// </remarks>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
public class RunWeaponCatalog : WeaponCatalog
{
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    public override WeaponCatalogType Type => WeaponCatalogType.Run;

    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog)]
    protected override void Define(List<IWeapon> weapons)
    {
        weapons.AddRange(
        [
            WeaponDefinitions.Colt45,
            WeaponDefinitions.Sawedoff,
            WeaponDefinitions.Uzi,
            WeaponDefinitions.Tec9
        ]);
    }
}
