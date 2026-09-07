namespace CTF.Application.GameRules.Core;

/// <summary>
/// The flag-capture reward amounts, governed solely by the CTF flag rules (CD-02).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public static class FlagCapturedRewards
{
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int EarnedCoins = 5;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int EarnedScore = 2;
}
