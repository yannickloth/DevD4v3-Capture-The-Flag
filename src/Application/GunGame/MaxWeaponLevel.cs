namespace CTF.Application.GunGameRules;

/// <summary>
/// Represents the highest weapon level available in a weapon progression.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public readonly struct MaxWeaponLevel
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public int Value { get; }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public MaxWeaponLevel(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);
        Value = value;
    }
}
