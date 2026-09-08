namespace CTF.Application.GameRules;

/// <summary>
/// The flag-return reward amounts, governed solely by the CTF flag rules (CD-02).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public static class FlagReturnedRewards
{
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int EarnedCoins = 5;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int EarnedScore = 2;
}
