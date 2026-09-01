namespace CTF.Application.Players.TopPlayers;

/// <summary>
/// Represents the configuration thresholds for qualifying top players.
/// </summary>
/// <remarks>Change drivers: CD-17 (game configuration/.env schema), CD-10 (player-statistics/rank model)</remarks>
public class TopPlayersSettings
{
    /// <summary>
    /// Gets the required total kills for a player to be considered in the top players.
    /// </summary>
    /// <remarks>Change drivers: CD-17 (game configuration/.env schema), CD-10 (player-statistics/rank model)</remarks>
    public int RequiredTotalKills { get; init; } = 150;

    /// <summary>
    /// Gets the required maximum killing spree for a player to be considered in the top players.
    /// </summary>
    /// <remarks>Change drivers: CD-17 (game configuration/.env schema), CD-10 (player-statistics/rank model)</remarks>
    public int RequiredMaxKillingSpree { get; init; } = 10;
}
