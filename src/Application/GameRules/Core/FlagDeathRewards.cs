namespace CTF.Application.GameRules.Core;

/// <summary>
/// The carrier-kill reward amounts, governed solely by the CTF flag rules (CD-02).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public static class FlagDeathRewards
{
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int CarrierKillEarnedCoins  = 4;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int CarrierKillEarnedHealth = 10;

    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public const int CarrierKillEarnedScore  = 2;
}
