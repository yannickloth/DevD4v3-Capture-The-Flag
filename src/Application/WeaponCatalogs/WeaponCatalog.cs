namespace CTF.Application.WeaponCatalogs;

/// <summary>
/// Represents a predefined collection of weapons available to players.
/// </summary>
/// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
/// <remarks>
/// Derived classes define which weapons belong to a specific catalog.
/// </remarks>
public abstract class WeaponCatalog
{
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration: catalog weapons)</remarks>
    private readonly List<IWeapon> _weapons = 
    [
        WeaponDefinitions.Knife,
        WeaponDefinitions.Parachute
    ];

    /// <summary>
    /// Initializes the catalog with the weapons that are
    /// always available to players.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    protected WeaponCatalog()
    {
        Define(_weapons);
    }

    /// <summary>
    /// Gets the catalog type represented by the implementation.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public abstract WeaponCatalogType Type { get; }

    /// <summary>
    /// Defines the weapons that belong to this catalog.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    protected abstract void Define(List<IWeapon> weapons);

    /// <summary>
    /// Gets the number of weapons in the catalog.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public int Count => _weapons.Count;

    /// <summary>
    /// Gets all weapons defined in this catalog.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public IReadOnlyList<IWeapon> GetAll() => _weapons;

    /// <summary>
    /// Determines whether the specified weapon belongs to this catalog.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public bool Contains(IWeapon weapon) => _weapons.Exists(w => w.Id == weapon.Id);

    /// <summary>
    /// Gets a weapon by its identifier.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public Result<IWeapon> GetById(Weapon id)
    {
        IWeapon weapon = _weapons.FirstOrDefault(w => w.Id == id);
        return weapon is null ? 
            Result<IWeapon>.Failure(Messages.WeaponNotFound) :
            Result<IWeapon>.Success(weapon);
    }

    /// <summary>
    /// Gets a weapon by its display name.
    /// </summary>
    /// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration)</remarks>
    public Result<IWeapon> GetByName(string weaponName)
    {
        ArgumentNullException.ThrowIfNull(weaponName);

        IWeapon weapon = _weapons.FirstOrDefault(
            w => w.Name.Equals(
                weaponName,
                StringComparison.OrdinalIgnoreCase));

        return weapon is null ? 
            Result<IWeapon>.Failure(Messages.WeaponNotFound) :
            Result<IWeapon>.Success(weapon);
    }
}
