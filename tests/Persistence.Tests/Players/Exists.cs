namespace Persistence.Tests.Players;

/// <summary>Verifies the repository Exists operation.</summary>
/// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.Exists); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
public class PlayerExists
{
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.Exists); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void Exists_WhenPlayerExists_ShouldReturnTrue(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "moderator_player";

        // Act
        bool actual = playerRepository.Exists(playerName);

        // Assert
        actual.Should().BeTrue();
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.Exists); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void Exists_WhenPlayerDoesNotExist_ShouldReturnFalse(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "NotFound";

        // Act
        bool actual = playerRepository.Exists(playerName);

        // Assert
        actual.Should().BeFalse();
    }
}
