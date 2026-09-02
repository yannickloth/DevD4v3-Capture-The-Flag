namespace Persistence.Tests.Players;

/// <summary>Verifies the top-players repository queries (by total kills and by max killing spree).</summary>
/// <remarks>Change drivers: CD-29 (root; code-under-test: ITopPlayersRepository.GetByTotalKills/GetByMaxKillingSpree); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29; CD-20 (outbound repository contract) → CD-29; CD-18 (database schema/player data model) → CD-29</remarks>
public class GetTopPlayers
{
    /// <remarks>Change drivers: CD-29 (root; code-under-test: ITopPlayersRepository.GetByTotalKills); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29; CD-20 (outbound repository contract) → CD-29</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void GetByTotalKills_WhenSeedDataIsAvailable_ShouldReturnPlayersOrderedByTotalKills(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        ITopPlayersRepository topPlayersRepository = repositoryManager.TopPlayersRepository;
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(6);
        TopPlayersByTotalKills[] expectedPlayers = 
        [
            new() { PlayerName = "Basic_Player(6)", TotalKills = 251, Rank = RankId.Hitman },
            new() { PlayerName = "Basic_Player(5)", TotalKills = 200, Rank = RankId.Advanced },
            new() { PlayerName = "Basic_Player(7)", TotalKills = 200, Rank = RankId.Advanced },
            new() { PlayerName = "Basic_Player(4)", TotalKills = 170, Rank = RankId.SemiAdvance },
            new() { PlayerName = "Basic_Player(3)", TotalKills = 160, Rank = RankId.SemiAdvance },
            new() { PlayerName = "Basic_Player(2)", TotalKills = 150, Rank = RankId.SemiAdvance }
        ];

        // Act
        TopPlayersByTotalKills[] actual = topPlayersRepository
            .GetByTotalKills(result.Value)
            .ToArray();

        // Assert
        actual.Should().BeEquivalentTo(expectedPlayers);
    }

    /// <remarks>Change drivers: CD-29 (root; code-under-test: ITopPlayersRepository.GetByTotalKills); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29; CD-20 (outbound repository contract) → CD-29</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void GetByTotalKills_WhenSeedDataIsNotAvailable_ShouldReturnEmptyCollection(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.RemoveSeedData();
        ITopPlayersRepository topPlayersRepository = repositoryManager.TopPlayersRepository;
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(6);

        // Act
        TopPlayersByTotalKills[] actual = topPlayersRepository
            .GetByTotalKills(result.Value)
            .ToArray();

        // Assert
        actual.Should().BeEmpty();
    }

    /// <remarks>Change drivers: CD-29 (root; code-under-test: ITopPlayersRepository.GetByMaxKillingSpree); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29; CD-20 (outbound repository contract) → CD-29</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void GetByMaxKillingSpree_WhenSeedDataIsAvailable_ShouldReturnPlayersOrderedByMaxKillingSpree(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        ITopPlayersRepository topPlayersRepository = repositoryManager.TopPlayersRepository;
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(6);
        TopPlayersByMaxKillingSpree[] expectedPlayers =
        [
            new() { PlayerName = "Basic_Player(6)", MaxKillingSpree = 50 },
            new() { PlayerName = "Basic_Player(7)", MaxKillingSpree = 30 },
            new() { PlayerName = "Basic_Player(5)", MaxKillingSpree = 25 },
            new() { PlayerName = "Basic_Player(4)", MaxKillingSpree = 20 },
            new() { PlayerName = "Basic_Player(3)", MaxKillingSpree = 15 },
            new() { PlayerName = "Basic_Player(2)", MaxKillingSpree = 10 }
        ];

        // Act
        TopPlayersByMaxKillingSpree[] actual = topPlayersRepository
            .GetByMaxKillingSpree(result.Value)
            .ToArray();

        // Assert
        actual.Should().BeEquivalentTo(expectedPlayers);
    }

    /// <remarks>Change drivers: CD-29 (root; code-under-test: ITopPlayersRepository.GetByMaxKillingSpree); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29; CD-20 (outbound repository contract) → CD-29</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void GetByMaxKillingSpree_WhenSeedDataIsNotAvailable_ShouldReturnEmptyCollection(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.RemoveSeedData();
        ITopPlayersRepository topPlayersRepository = repositoryManager.TopPlayersRepository;
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(6);

        // Act
        TopPlayersByMaxKillingSpree[] actual = topPlayersRepository
            .GetByMaxKillingSpree(result.Value)
            .ToArray();

        // Assert
        actual.Should().BeEmpty();
    }
}
