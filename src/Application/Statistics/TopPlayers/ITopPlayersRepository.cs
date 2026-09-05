namespace CTF.Application.Statistics.TopPlayers;

/// <summary>
/// Represents the persistence contract for retrieving top players.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: top-players leaderboard); CD-20 (outbound repository contract) → CD-10</remarks>
public interface ITopPlayersRepository
{
    /// <summary>
    /// Retrieves a collection of top players sorted by total kills.
    /// </summary>
    /// <param name="maxPlayers">The maximum number of players to retrieve.</param>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: top-players leaderboard); CD-20 (outbound repository contract) → CD-10</remarks>
    IEnumerable<TopPlayersByTotalKills> GetByTotalKills(MaxTopPlayers maxPlayers);

    /// <summary>
    /// Retrieves a collection of top players sorted by maximum killing sprees.
    /// </summary>
    /// <param name="maxPlayers">The maximum number of players to retrieve.</param>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: top-players leaderboard); CD-20 (outbound repository contract) → CD-10</remarks>
    IEnumerable<TopPlayersByMaxKillingSpree> GetByMaxKillingSpree(MaxTopPlayers maxPlayers);
}
