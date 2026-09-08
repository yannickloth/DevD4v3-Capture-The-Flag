namespace CTF.Application.GameRules;

/// <summary>
/// The flag-score reward amounts, governed solely by the CTF flag rules (CD-02).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public static class FlagScoreRewards
{
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int CarrierEarnedCoins = 8;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int CarrierEarnedScore = 4;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int TeamEarnedCoins    = 5;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int TeamEarnedHealth   = 10;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int TeamEarnedScore    = 1;
}
