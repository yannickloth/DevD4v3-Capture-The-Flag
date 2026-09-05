namespace Persistence.InMemory.Repositories.TopPlayers;

/// <remarks>Injected dependencies (change drivers of these elements): players (Dictionary&lt;int, FakePlayer&gt;) -> CD-18; settings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Configuration)]
internal class FakeTopPlayersRepository(
    Dictionary<int, FakePlayer> players,
    TopPlayersSettings settings) : ITopPlayersRepository
{
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Configuration)]
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

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Configuration)]
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
