namespace CTF.Application.Players.Weapons.Catalogs;

/// <summary>
/// Defines a weapon catalog that allows players to remain mobile while fighting.
/// </summary>
/// <remarks>Change drivers: CD-04 (root; root; weapon-catalog configuration)</remarks>
/// <remarks>
/// These weapons support the classic Run Weapons (RW) gameplay style,
/// where players can move quickly while attacking.
/// </remarks>
public class RunWeaponCatalog : WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (root; root; weapon-catalog configuration)</remarks>
    public override WeaponCatalogType Type => WeaponCatalogType.Run;

    /// <remarks>Change drivers: CD-04 (root; root; weapon-catalog configuration)</remarks>
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
