namespace CTF.Application.GunGames.WeaponProgressions;

/// <summary>
/// Represents a player's current weapon level within a weapon progression.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public readonly struct WeaponLevel 
    : IComparable<WeaponLevel>, IEquatable<WeaponLevel>
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public int Value { get; }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public static WeaponLevel First { get; } = new(1);

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    private WeaponLevel(int value)
        => Value = value;

    /// <summary>
    /// Advances to the next weapon level without exceeding the specified maximum.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public WeaponLevel Next(MaxWeaponLevel maxLevel)
        => new(Value < maxLevel.Value ? Value + 1 : Value);

    /// <summary>
    /// Moves to the previous weapon level.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public WeaponLevel Previous()
        => new(Value > 1 ? Value - 1 : Value);

    /// <summary>
    /// Determines whether this is the final weapon level.
    /// </summary>
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public bool IsMax(MaxWeaponLevel maxLevel) 
        => Value == maxLevel.Value;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override string ToString()
        => $"{Value}";

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public int CompareTo(WeaponLevel other)
        => Value.CompareTo(other.Value);

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public bool Equals(WeaponLevel other)
        => Value == other.Value;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override bool Equals(object obj)
        => obj is WeaponLevel other && Equals(other);

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public override int GetHashCode()
        => Value.GetHashCode();

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public static bool operator >(WeaponLevel left, WeaponLevel right)
        => left.Value > right.Value;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public static bool operator <(WeaponLevel left, WeaponLevel right)
        => left.Value < right.Value;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public static bool operator >=(WeaponLevel left, WeaponLevel right)
        => left.Value >= right.Value;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public static bool operator <=(WeaponLevel left, WeaponLevel right)
        => left.Value <= right.Value;

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public static bool operator ==(WeaponLevel left, WeaponLevel right)
        => left.Equals(right);

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public static bool operator !=(WeaponLevel left, WeaponLevel right)
        => !left.Equals(right);
}
