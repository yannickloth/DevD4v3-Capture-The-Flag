namespace CTF.Application.GunGames;

/// <summary>
/// Represents a player's current weapon level within a weapon progression.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public readonly struct WeaponLevel 
    : IComparable<WeaponLevel>, IEquatable<WeaponLevel>
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public int Value { get; }

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static WeaponLevel First { get; } = new(1);

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    private WeaponLevel(int value)
        => Value = value;

    /// <summary>
    /// Advances to the next weapon level without exceeding the specified maximum.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public WeaponLevel Next(MaxWeaponLevel maxLevel)
        => new(Value < maxLevel.Value ? Value + 1 : Value);

    /// <summary>
    /// Moves to the previous weapon level.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public WeaponLevel Previous()
        => new(Value > 1 ? Value - 1 : Value);

    /// <summary>
    /// Determines whether this is the final weapon level.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public bool IsMax(MaxWeaponLevel maxLevel) 
        => Value == maxLevel.Value;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override string ToString()
        => $"{Value}";

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public int CompareTo(WeaponLevel other)
        => Value.CompareTo(other.Value);

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public bool Equals(WeaponLevel other)
        => Value == other.Value;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override bool Equals(object obj)
        => obj is WeaponLevel other && Equals(other);

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public override int GetHashCode()
        => Value.GetHashCode();

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static bool operator >(WeaponLevel left, WeaponLevel right)
        => left.Value > right.Value;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static bool operator <(WeaponLevel left, WeaponLevel right)
        => left.Value < right.Value;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static bool operator >=(WeaponLevel left, WeaponLevel right)
        => left.Value >= right.Value;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static bool operator <=(WeaponLevel left, WeaponLevel right)
        => left.Value <= right.Value;

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static bool operator ==(WeaponLevel left, WeaponLevel right)
        => left.Equals(right);

    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public static bool operator !=(WeaponLevel left, WeaponLevel right)
        => !left.Equals(right);
}
