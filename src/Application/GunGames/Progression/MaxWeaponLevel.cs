namespace CTF.Application.GunGames;

/// <summary>
/// Represents the highest weapon level available in a weapon progression.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
public readonly struct MaxWeaponLevel
{
    [ChangeDriversAttribute(ChangeDriver.GunGame)]
    public int Value { get; }

    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules)</remarks>
    public MaxWeaponLevel(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);
        Value = value;
    }
}
