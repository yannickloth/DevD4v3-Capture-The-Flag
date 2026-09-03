namespace CTF.Application.Statistics;

/// <summary>
/// Provides killing-spree evaluation extension methods.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
public static class PlayerKillingSpreeEvaluator
{
    /// <summary>
    /// Determines whether the specified player has surpassed their previously
    /// recorded maximum killing spree.
    /// </summary>
    /// <param name="playerInfo">The player to evaluate.</param>
    /// <returns>
    /// <see langword="true"/> if the current spree exceeds the stored maximum;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public static bool HasSurpassedMaxKillingSpree(this PlayerInfo playerInfo)
        => playerInfo.StatsPerRound.KillingSpree > playerInfo.MaxKillingSpree;
}
