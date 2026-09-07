namespace CTF.Application.Statistics.Core;

/// <summary>
/// The killing-spree rule constants and predicate, governed solely by the
/// player-statistics/rank model (CD-10).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public static class PlayerKillingSpreeRewards
{
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public const int MinimumKillingSpree = 2;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public const int EarnedCoins = 20;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public const int EarnedHealth = 10;

    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public const int ConsecutiveKillsBonusHealth = 40;

    /// <summary>Determines whether the player has surpassed their previously recorded maximum killing spree.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public static bool HasSurpassedMaxKillingSpree(PlayerInfo playerInfo)
        => playerInfo.Stats.PerRound.KillingSpree > playerInfo.Stats.MaxKillingSpree;
}
