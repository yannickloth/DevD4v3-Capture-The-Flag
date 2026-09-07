namespace CTF.Application.Statistics.Core;

/// <summary>
/// The rank-up award amounts, governed solely by the player-statistics/rank model (CD-10).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public static class PlayerRankUpRewards
{
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public const int EarnedHealth = 100;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public const int EarnedArmour = 100;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public const int EarnedCoins  = 100;
}
