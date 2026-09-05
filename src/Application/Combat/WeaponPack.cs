namespace CTF.Application.Combat;

/// <summary>
/// Represents a collection of weapons where only one weapon
/// can occupy a slot at a time.
/// </summary>
/// <remarks>
/// GTA San Andreas allows only one weapon per slot. Adding a weapon
/// replaces any existing weapon occupying the same slot.
/// </remarks>
[ChangeDriversAttribute(ChangeDriver.Combat)]
public class WeaponPack : IEnumerable<IWeapon>
{
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    private readonly List<IWeapon> _weapons = [];

    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public int TotalItems => _weapons.Count;
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public IWeapon this[int index] => _weapons[index];
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public bool IsEmpty() => _weapons.Count == 0;

    [ChangeDriversAttribute(ChangeDriver.Combat)]
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

    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public void Remove(IWeapon weapon) => _weapons.Remove(weapon);
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public int RemoveAll(Predicate<IWeapon> predicate) => _weapons.RemoveAll(predicate);
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public bool Exists(IWeapon weapon) => _weapons.Find(w => w == weapon) is not null;
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public void Clear() => _weapons.Clear();
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    public IEnumerator<IWeapon> GetEnumerator() => _weapons.GetEnumerator();
    [ChangeDriversAttribute(ChangeDriver.Combat)]
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
