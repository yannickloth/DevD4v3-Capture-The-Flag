namespace CTF.Application.Statistics;

/// <summary>
/// Represents a top player entry ranked by maximum killing spree.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
public class TopPlayersByMaxKillingSpree
{
    /// <summary>Gets the player name.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public string PlayerName { get; init; }

    /// <summary>Gets the maximum killing spree.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public int MaxKillingSpree { get; init; }
}
