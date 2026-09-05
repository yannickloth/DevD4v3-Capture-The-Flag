namespace CTF.Application.WeaponCatalogs.Catalogs.Settings;

/// <summary>
/// Represents the weapon catalog configuration currently used by the server.
/// The active catalog can be changed at runtime.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
public class WeaponCatalogSettings
{
    /// <summary>
    /// Gets or sets the catalog currently used by the server.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    public WeaponCatalogType Type { get; private set; }

    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration); CD-17 (game configuration/.env schema) → CD-04</remarks>
    public WeaponCatalogSettings(WeaponCatalogType type = WeaponCatalogType.Walking)
    {
        WeaponCatalogTypeValidator.EnsureValidCatalog(type);
        Type = type;
    }

    /// <summary>
    /// Changes the active weapon catalog used by the server.
    /// </summary>
    /// <param name="type">
    /// The weapon catalog to activate.
    /// </param>
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
    public void Change(WeaponCatalogType type)
    {
        WeaponCatalogTypeValidator.EnsureValidCatalog(type);
        Type = type;
    }

}
