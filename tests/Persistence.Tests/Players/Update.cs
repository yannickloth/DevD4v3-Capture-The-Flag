namespace Persistence.Tests.Players;

/// <summary>Verifies each repository update operation persists the changed field.</summary>
/// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateX); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
public class UpdatePlayer
{
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateName); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdatePlayerName(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var oldName = "Moderator_Player";
        var newName = "Player1";
        PlayerInfo playerInfo = playerRepository.GetOrDefault(oldName);
        playerInfo.SetName(newName);

        // Act
        playerRepository.UpdateName(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(newName);

        // Assert
        actual.Name.Should().Be(newName);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdatePassword); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdatePlayerPassword(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        var expectedPassword = "D123456$";
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetPassword(expectedPassword);

        // Act
        playerRepository.UpdatePassword(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Password.Should().Be(expectedPassword);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateTotalKills); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateTotalKills(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedTotalKills = 20;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetTotalKills(expectedTotalKills);

        // Act
        playerRepository.UpdateTotalKills(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.TotalKills.Should().Be(expectedTotalKills);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateTotalDeaths); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateTotalDeaths(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedTotalDeaths = 100;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetTotalDeaths(expectedTotalDeaths);

        // Act
        playerRepository.UpdateTotalDeaths(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.TotalDeaths.Should().Be(expectedTotalDeaths);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateMaxKillingSpree); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateMaxKillingSpree(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedKillingSpree = 25;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetMaxKillingSpree(expectedKillingSpree);

        // Act
        playerRepository.UpdateMaxKillingSpree(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.MaxKillingSpree.Should().Be(expectedKillingSpree);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateBroughtFlags); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateBroughtFlags(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedBroughtFlags = 2;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.AddBroughtFlags();
        playerInfo.AddBroughtFlags();

        // Act
        playerRepository.UpdateBroughtFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.BroughtFlags.Should().Be(expectedBroughtFlags);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateCapturedFlags); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateCapturedFlags(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedCapturedFlags = 2;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.AddCapturedFlags();
        playerInfo.AddCapturedFlags();

        // Act
        playerRepository.UpdateCapturedFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.CapturedFlags.Should().Be(expectedCapturedFlags);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateDroppedFlags); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateDroppedFlags(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedDroppedFlags = 2;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.AddDroppedFlags();
        playerInfo.AddDroppedFlags();

        // Act
        playerRepository.UpdateDroppedFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.DroppedFlags.Should().Be(expectedDroppedFlags);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateReturnedFlags); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateReturnedFlags(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedReturnedFlags = 2;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.AddReturnedFlags();
        playerInfo.AddReturnedFlags();

        // Act
        playerRepository.UpdateReturnedFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.ReturnedFlags.Should().Be(expectedReturnedFlags);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateHeadShots); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateHeadShots(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedHeadShots = 2;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.AddHeadShots();
        playerInfo.AddHeadShots();

        // Act
        playerRepository.UpdateHeadShots(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.HeadShots.Should().Be(expectedHeadShots);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateGunGameWins); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateGunGameWins(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedGunGameWins = 2;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.AddGunGameWins();
        playerInfo.AddGunGameWins();

        // Act
        playerRepository.UpdateGunGameWins(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.GunGameWins.Should().Be(expectedGunGameWins);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateRole); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateRole(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        RoleId expectedRoleId = RoleId.Admin;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetRole(expectedRoleId);

        // Act
        playerRepository.UpdateRole(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.RoleId.Should().Be(expectedRoleId);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateSkin); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateSkin(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        int expectedSkinId = 100;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetSkin(expectedSkinId);

        // Act
        playerRepository.UpdateSkin(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.SkinId.Should().Be(expectedSkinId);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateRank); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateRank(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        RankId expectedRankId = RankId.GameMaster;
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetRank(expectedRankId);

        // Act
        playerRepository.UpdateRank(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.RankId.Should().Be(expectedRankId);
    }

    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.UpdateLastConnection); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void ShouldUpdateLastConnection(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerName = "Moderator_Player";
        PlayerInfo playerInfo = playerRepository.GetOrDefault(playerName);
        playerInfo.SetLastConnection();

        // Act
        playerRepository.UpdateLastConnection(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.LastConnection.Should().BeSameDateAs(playerInfo.LastConnection);
    }
}
