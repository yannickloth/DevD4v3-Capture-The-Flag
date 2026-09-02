namespace Persistence.InMemory;

/// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-21 (DI container/composition) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
public static class PersistenceInMemoryServicesExtensions
{
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-21 (DI container/composition) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
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
