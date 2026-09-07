namespace CTF.Application.WeaponCatalogs.ActiveCatalog;

/// <summary>
/// Represents the active weapon catalog used by the server.
/// </summary>
/// <remarks>
/// Consumers do not need to know which weapon catalog is active.
/// This class always exposes the catalog selected by the current server configuration.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): settings -> CD-17; catalogs (FrozenDictionary&lt;WeaponCatalogType, WeaponCatalog&gt;) -> CD-04. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
public class ActiveWeaponCatalog(
    WeaponCatalogSettings settings, 
    FrozenDictionary<WeaponCatalogType, WeaponCatalog> catalogs)
{
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    private WeaponCatalog Current 
        => catalogs[settings.Type];

    /// <inheritdoc cref="WeaponCatalog.Count"/>
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    public int Count 
        => Current.Count;

    /// <inheritdoc cref="WeaponCatalog.GetAll"/>
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    public IReadOnlyList<IWeapon> GetAll()
        => Current.GetAll();

    /// <inheritdoc cref="WeaponCatalog.Contains"/>
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    public bool Contains(IWeapon weapon)
        => Current.Contains(weapon);

    /// <inheritdoc cref="WeaponCatalog.GetById"/>
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    public Result<IWeapon> GetById(Weapon id)
        => Current.GetById(id);

    /// <inheritdoc cref="WeaponCatalog.GetByName"/>
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    public Result<IWeapon> GetByName(string weaponName)
        => Current.GetByName(weaponName);
}
