namespace Persistence.Tests.Players;

/// <summary>Verifies the repository GetOrDefault operation returns the persisted player or null.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.DatabaseSchema)]
public class GetPlayerOrDefault
{
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.DatabaseSchema)]
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

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.DatabaseSchema)]
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
