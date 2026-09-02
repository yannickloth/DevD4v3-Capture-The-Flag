namespace CTF.Application.Players.Weapons;

/// <summary>
/// Represents a collection of weapons where only one weapon
/// can occupy a slot at a time.
/// </summary>
/// <remarks>Change drivers: CD-03 (combat/weapon-rules specification), CD-01 (open.mp/SampSharp platform API)</remarks>
/// <remarks>
/// GTA San Andreas allows only one weapon per slot. Adding a weapon
/// replaces any existing weapon occupying the same slot.
/// </remarks>
public class WeaponPack : IEnumerable<IWeapon>
{
    private readonly List<IWeapon> _weapons = [];

    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public int TotalItems => _weapons.Count;
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public IWeapon this[int index] => _weapons[index];
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public bool IsEmpty() => _weapons.Count == 0;

    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification), CD-01 (open.mp/SampSharp platform API)</remarks>
    public void Add(IWeapon weapon)
    {
        ArgumentNullException.ThrowIfNull(weapon);
        // GTA San Andreas does not allow a player to have two weapons with the same slot.
        // Checks if there is no weapon with the same slot in the player's weapon pack.
        int index = _weapons.FindIndex(w => w.Slot == weapon.Slot);
        bool hasWeaponWithSameSlot = index != -1;
        if (hasWeaponWithSameSlot)
            _weapons[index] = weapon;
        else
            _weapons.Add(weapon);
    }

    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public void Remove(IWeapon weapon) => _weapons.Remove(weapon);
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public int RemoveAll(Predicate<IWeapon> predicate) => _weapons.RemoveAll(predicate);
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public bool Exists(IWeapon weapon) => _weapons.Find(w => w == weapon) is not null;
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public void Clear() => _weapons.Clear();
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    public IEnumerator<IWeapon> GetEnumerator() => _weapons.GetEnumerator();
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification)</remarks>
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
