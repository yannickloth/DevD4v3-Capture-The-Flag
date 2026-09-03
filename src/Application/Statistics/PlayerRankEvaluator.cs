namespace CTF.Application.Statistics;

/// <summary>
/// Provides player-rank evaluation extension methods.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
public static class PlayerRankEvaluator
{
    /// <summary>
    /// Determines whether the specified player can advance to the next rank tier.
    /// </summary>
    /// <param name="playerInfo">The player to evaluate.</param>
    /// <returns>
    /// <see langword="true"/> if the player has enough total kills for the next rank;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public static bool CanMoveUpToNextRank(this PlayerInfo playerInfo)
    {
        Rank currentRank = RankCollection.GetById(playerInfo.RankId).Value;
        if (currentRank.IsMax())
            return false;

        Rank nextRank = RankCollection.GetNextRank(playerInfo.RankId).Value;
        return playerInfo.TotalKills >= nextRank.RequiredKills;
    }
}
