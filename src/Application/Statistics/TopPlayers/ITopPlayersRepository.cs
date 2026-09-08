namespace CTF.Application.Statistics.RepositoryNsNs2;

/// <summary>
/// Represents the persistence contract for retrieving top players.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
public interface ITopPlayersRepository
{
    /// <summary>
    /// Retrieves a collection of top players sorted by total kills.
    /// </summary>
    /// <param name="maxPlayers">The maximum number of players to retrieve.</param>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    IEnumerable<TopPlayersByTotalKills> GetByTotalKills(MaxTopPlayers maxPlayers);

    /// <summary>
    /// Retrieves a collection of top players sorted by maximum killing sprees.
    /// </summary>
    /// <param name="maxPlayers">The maximum number of players to retrieve.</param>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    IEnumerable<TopPlayersByMaxKillingSpree> GetByMaxKillingSpree(MaxTopPlayers maxPlayers);
}
