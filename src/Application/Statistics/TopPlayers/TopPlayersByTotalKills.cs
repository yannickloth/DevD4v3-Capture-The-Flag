namespace CTF.Application.Statistics;

/// <summary>
/// Represents a top player entry ranked by total kills.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public class TopPlayersByTotalKills
{
    /// <summary>Gets the player name.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public string PlayerName { get; init; }

    /// <summary>Gets the total kills.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int TotalKills { get; init; }

    /// <summary>Gets the player's rank.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public RankId Rank { get; init; }
}
