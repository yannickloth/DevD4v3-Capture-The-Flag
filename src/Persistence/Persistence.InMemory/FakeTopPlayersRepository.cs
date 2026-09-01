namespace Persistence.InMemory;

/// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model), CD-25 (BCrypt password-hashing contract), CD-21 (DI container/composition)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): players (Dictionary&lt;int, FakePlayer&gt;) -> CD-18; settings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
internal class FakeTopPlayersRepository(
    Dictionary<int, FakePlayer> players,
    TopPlayersSettings settings) : ITopPlayersRepository
{
    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public IEnumerable<TopPlayersByMaxKillingSpree> GetByMaxKillingSpree(MaxTopPlayers maxPlayers)
        => players
            .Where(kvp => kvp.Value.MaxKillingSpree >= settings.RequiredMaxKillingSpree)
            .OrderByDescending(x => x.Value.MaxKillingSpree)
            .Select(kvp => new TopPlayersByMaxKillingSpree
            {
                PlayerName      = kvp.Value.Name,
                MaxKillingSpree = kvp.Value.MaxKillingSpree
            })
            .Take(maxPlayers.Value)
            .ToArray();

    /// <remarks>Change drivers: CD-20 (outbound repository contract), CD-18 (database schema/player data model)</remarks>
    public IEnumerable<TopPlayersByTotalKills> GetByTotalKills(MaxTopPlayers maxPlayers)
        => players
            .Where(kvp => kvp.Value.TotalKills >= settings.RequiredTotalKills)
            .OrderByDescending(kvp => kvp.Value.TotalKills)
            .Select(kvp => new TopPlayersByTotalKills
            {
                PlayerName = kvp.Value.Name,
                TotalKills = kvp.Value.TotalKills,
                Rank       = kvp.Value.RankId
            })
            .Take(maxPlayers.Value)
            .ToArray();
}
