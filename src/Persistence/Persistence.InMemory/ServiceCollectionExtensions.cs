namespace Persistence.InMemory;

/// <remarks>Change drivers: CD-21 (root; DI container/composition); CD-18 (database schema/player data model) → CD-21</remarks>
public static class PersistenceInMemoryServicesExtensions
{
    /// <remarks>Change drivers: CD-21 (root; DI container/composition); CD-18 (database schema/player data model) → CD-21</remarks>
    public static IServiceCollection AddPersistenceInMemoryServices(
        this IServiceCollection services)
    {
        PlayerIdValueGenerator.Instance.Reset();
        Dictionary<int, FakePlayer> players = FakePlayerSeedData.Create();
        services.AddSingleton<IPlayerRepository, FakePlayerRepository>();
        services.AddSingleton<ITopPlayersRepository, FakeTopPlayersRepository>();
        services.AddSingleton(players);
        return services;
    }
}
