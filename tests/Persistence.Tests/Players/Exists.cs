namespace Persistence.Tests.Players;

/// <summary>Verifies the repository Exists operation.</summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.DatabaseSchema)]
public class PlayerExists
{
    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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

    [ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
