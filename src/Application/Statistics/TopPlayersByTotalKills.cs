namespace CTF.Application.Statistics;

/// <summary>
/// Represents a top player entry ranked by total kills.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
public class TopPlayersByTotalKills
{
    /// <summary>Gets the player name.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public string PlayerName { get; init; }

    /// <summary>Gets the total kills.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public int TotalKills { get; init; }

    /// <summary>Gets the player's rank.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public RankId Rank { get; init; }
}
