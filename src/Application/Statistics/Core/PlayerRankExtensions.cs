namespace CTF.Application.Statistics.Core;

/// <summary>
/// Provides player rank query extension methods.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public static class PlayerRankExtensions
{
    /// <summary>Determines whether the player has the specified rank tier.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public static bool HasRank(this PlayerInfo playerInfo, RankId id)
        => playerInfo.Stats.RankId == id;
}
