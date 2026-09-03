namespace CTF.Application.Statistics;

/// <summary>
/// Provides player rank query extension methods.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
public static class PlayerRankExtensions
{
    /// <summary>Determines whether the player has the specified rank tier.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public static bool HasRank(this PlayerInfo playerInfo, RankId id)
        => playerInfo.RankId == id;
}
