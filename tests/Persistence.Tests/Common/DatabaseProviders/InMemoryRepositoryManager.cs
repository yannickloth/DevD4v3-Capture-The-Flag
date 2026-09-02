namespace Persistence.Tests.Common.DatabaseProviders;

/// <summary>Wires the in-memory persistence stack for the repository tests.</summary>
/// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract via the fake hasher); CD-20 (outbound repository contract); CD-18 (database schema/player data model: the <c>Dictionary&lt;int,FakePlayer&gt;</c> store); CD-21 (DI container/composition); CD-29 (code-under-test: the in-memory repository seam)</remarks>
public class InMemoryRepositoryManager : IRepositoryManager
{
    private readonly ServiceProvider _serviceProvider;

    /// <remarks>Change drivers: CD-20 (outbound repository contract)</remarks>
    public IPlayerRepository PlayerRepository { get; }
    /// <remarks>Change drivers: CD-20 (outbound repository contract)</remarks>
    public ITopPlayersRepository TopPlayersRepository { get; }
    /// <remarks>Change drivers: CD-25 (BCrypt password-hashing contract); CD-20 (outbound repository contract); CD-18 (database schema/player data model); CD-21 (DI container/composition)</remarks>
    public InMemoryRepositoryManager()
    {
        var services = new ServiceCollection();
        services.AddSingleton(new TopPlayersSettings());
        services.AddSingleton<IPasswordHasher, FakePasswordHasher>();
        services.AddPersistenceInMemoryServices();
        _serviceProvider = services.BuildServiceProvider();
        PlayerRepository = _serviceProvider.GetRequiredService<IPlayerRepository>();
        TopPlayersRepository = _serviceProvider.GetRequiredService<ITopPlayersRepository>();
    }

    /// <remarks>Change drivers: CD-21 (DI container/composition)</remarks>
    public void Dispose()
    {
        _serviceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <remarks>Change drivers: CD-18 (database schema/player data model: seed-data lifecycle)</remarks>
    public void InitializeSeedData()
    {

    }

    /// <remarks>Change drivers: CD-18 (database schema/player data model)</remarks>
    public void RemoveSeedData()
    {
        _serviceProvider
            .GetRequiredService<Dictionary<int, FakePlayer>>()
            .Clear();
    }
}
