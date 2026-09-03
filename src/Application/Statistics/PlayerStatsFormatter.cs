namespace CTF.Application.Statistics;

/// <summary>
/// Formats player statistics as a textdraw-compatible string.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-01 (open.mp/SampSharp platform API: textdraw color/format) → CD-10</remarks>
public static class PlayerStatsFormatter
{
    /// <summary>
    /// Gets the player's statistics formatted for display in a textdraw.
    /// </summary>
    /// <param name="playerInfo">The player whose statistics should be formatted.</param>
    /// <returns>A textdraw-formatted statistics string.</returns>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public static string GetStatsAsText(this PlayerInfo playerInfo)
    {
        Result<Rank> rankResult = RankCollection.GetById(playerInfo.RankId);
        var stats = new
        {
            playerInfo.StatsPerRound.Kills,
            playerInfo.StatsPerRound.Deaths,
            playerInfo.StatsPerRound.KillingSpree,
            playerInfo.StatsPerRound.Coins,
            MaxRank = RankCollection.Count,
            Level = (int)playerInfo.RankId + 1,
            RankName = rankResult.Value.Name
        };
        const string message =
            "~w~KILLS: ~y~{Kills} ~w~DEATHS: ~y~{Deaths} ~w~SPREE: ~y~{KillingSpree} " +
            "~w~COINS: ~y~{Coins}/100 ~w~LEVEL: ~y~{Level}/{MaxRank} ~w~RANK: ~y~{RankName}";
        return Smart.Format(message, stats);
    }
}
