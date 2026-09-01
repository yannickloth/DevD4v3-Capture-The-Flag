namespace CTF.Application.Players.Weapons;

/// <summary>
/// Represents the active weapon catalog used by the server.
/// </summary>
/// <remarks>Change drivers: CD-04 (weapon-catalog configuration), CD-17 (game configuration/.env schema)</remarks>
/// <remarks>
/// Consumers do not need to know which weapon catalog is active.
/// This class always exposes the catalog selected by the current server configuration.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): settings -> CD-17; catalogs (FrozenDictionary&lt;WeaponCatalogType, WeaponCatalog&gt;) -> CD-04. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class ActiveWeaponCatalog(
    WeaponCatalogSettings settings, 
    FrozenDictionary<WeaponCatalogType, WeaponCatalog> catalogs)
{
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration), CD-17 (game configuration/.env schema)</remarks>
    private WeaponCatalog Current 
        => catalogs[settings.Type];

    /// <inheritdoc cref="WeaponCatalog.Count"/>
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    public int Count 
        => Current.Count;

    /// <inheritdoc cref="WeaponCatalog.GetAll"/>
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    public IReadOnlyList<IWeapon> GetAll()
        => Current.GetAll();

    /// <inheritdoc cref="WeaponCatalog.Contains"/>
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    public bool Contains(IWeapon weapon)
        => Current.Contains(weapon);

    /// <inheritdoc cref="WeaponCatalog.GetById"/>
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    public Result<IWeapon> GetById(Weapon id)
        => Current.GetById(id);

    /// <inheritdoc cref="WeaponCatalog.GetByName"/>
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration)</remarks>
    public Result<IWeapon> GetByName(string weaponName)
        => Current.GetByName(weaponName);
}
