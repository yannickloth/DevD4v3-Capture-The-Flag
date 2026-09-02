namespace CTF.Application.GunGames.WeaponProgressions;

/// <summary>
/// Represents a weapon progression consisting of an ordered sequence of weapons.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public abstract class WeaponProgression
{
    private readonly List<IWeapon> _weapons = [];

    /// <summary>
    /// Gets the type of weapon progression.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public abstract WeaponProgressionType Type { get; }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    protected WeaponProgression()
    {
        Define(_weapons);
        if (_weapons.Count == 0)
            throw new InvalidOperationException(
                "A weapon progression must define at least one weapon.");
    }

    /// <summary>
    /// Defines the weapon sequence for this progression.
    /// Weapons must be added in ascending level order,
    /// starting from the first weapon level.
    /// </summary>
    /// <param name="weapons">
    /// The collection to populate with weapons in progression order.
    /// </param>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    protected abstract void Define(List<IWeapon> weapons);

    /// <summary>
    /// Gets the maximum weapon level defined by this progression.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public MaxWeaponLevel MaxLevel
        => new(_weapons.Count);

    /// <summary>
    /// Determines whether the specified weapon level is the final level
    /// of this progression.
    /// </summary>
    /// <param name="level">
    /// The weapon level to evaluate.
    /// </param>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public bool IsFinalLevel(WeaponLevel level)
        => level.Value == MaxLevel.Value;

    /// <summary>
    /// Gets the weapon associated with the specified weapon level.
    /// </summary>
    /// <param name="level">
    /// The weapon level.
    /// </param>
    /// <returns>
    /// The weapon assigned to the specified level.
    /// </returns>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public IWeapon GetWeapon(WeaponLevel level)
    {
        int index = level.Value - 1;
        if (index < 0 || index >= _weapons.Count)
            throw new InvalidOperationException($"No weapon defined for level {level.Value}");

        return _weapons[index];
    }
}
