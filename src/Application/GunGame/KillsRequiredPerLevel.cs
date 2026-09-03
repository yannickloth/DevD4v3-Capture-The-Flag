namespace CTF.Application.GunGameRules;

/// <summary>
/// Represents the number of kills required to advance to the next weapon level.
/// </summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
public readonly struct KillsRequiredPerLevel
{
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public int Value { get; }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public KillsRequiredPerLevel(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);
        Value = value;
    }
}
