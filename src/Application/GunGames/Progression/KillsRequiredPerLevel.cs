namespace CTF.Application.GunGames;

/// <summary>
/// Represents the number of kills required to advance to the next weapon level.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public readonly struct KillsRequiredPerLevel
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public int Value { get; }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public KillsRequiredPerLevel(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);
        Value = value;
    }
}
