namespace CTF.Application.Tests.Repositories.DatabaseSchema.Composition.BCrypt;

/// <summary>Wires the in-memory persistence stack for the repository tests.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
public class InMemoryRepositoryManager : IRepositoryManager
{
    private readonly ServiceProvider _serviceProvider;

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public IPlayerRepository PlayerRepository { get; }
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public ITopPlayersRepository TopPlayersRepository { get; }
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract); CD-18 (database schema/player data model) → CD-20; CD-21 (DI container/composition) → CD-20; CD-25 (BCrypt password-hashing contract) → CD-20</remarks>
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

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public void Dispose()
    {
        _serviceProvider.Dispose();
        GC.SuppressFinalize(this);
    }

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public void InitializeSeedData()
    {

    }

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
    public void RemoveSeedData()
    {
        _serviceProvider
            .GetRequiredService<Dictionary<int, FakePlayer>>()
            .Clear();
    }
}
