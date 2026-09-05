namespace Persistence.InMemory;

[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.DatabaseSchema)]
public static class PersistenceInMemoryServicesExtensions
{
    [ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.DatabaseSchema)]
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
