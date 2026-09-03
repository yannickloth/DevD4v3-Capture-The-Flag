namespace Persistence.Tests.Players;

/// <summary>Verifies the repository GetOrDefault operation returns the persisted player or null.</summary>
/// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.GetOrDefault); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
public class GetPlayerOrDefault
{
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.GetOrDefault); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void GetOrDefault_WhenPlayerExists_ShouldReturnPlayerInfo(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "moderator_player";

        // Act
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Asserts
        actual.Account.AccountId.Should().Be(2);
        actual.Account.Name.Should().Be("Moderator_Player");
        actual.Role.Id.Should().Be(RoleId.Moderator);
        actual.Stats.RankId.Should().Be(RankId.Noob);
        actual.Appearance.SkinId.Should().Be(146);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.GetOrDefault); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void GetOrDefault_WhenPlayerDoesNotExist_ShouldReturnNull(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "NotFound";

        // Act
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Should().BeNull();
    }
}
