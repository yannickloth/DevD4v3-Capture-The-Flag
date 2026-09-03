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
        playerInfo.Account.SetName(newName);

        // Act
        playerRepository.UpdateName(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(newName);

        // Assert
        actual.Account.Name.Should().Be(newName);
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
        playerInfo.Account.SetPassword(expectedPassword);

        // Act
        playerRepository.UpdatePassword(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Account.Password.Should().Be(expectedPassword);
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
        playerInfo.Stats.SetTotalKills(expectedTotalKills);

        // Act
        playerRepository.UpdateTotalKills(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.TotalKills.Should().Be(expectedTotalKills);
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
        playerInfo.Stats.SetTotalDeaths(expectedTotalDeaths);

        // Act
        playerRepository.UpdateTotalDeaths(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.TotalDeaths.Should().Be(expectedTotalDeaths);
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
        playerInfo.Stats.SetMaxKillingSpree(expectedKillingSpree);

        // Act
        playerRepository.UpdateMaxKillingSpree(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.MaxKillingSpree.Should().Be(expectedKillingSpree);
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
        playerInfo.Stats.AddBroughtFlags();
        playerInfo.Stats.AddBroughtFlags();

        // Act
        playerRepository.UpdateBroughtFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.BroughtFlags.Should().Be(expectedBroughtFlags);
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
        playerInfo.Stats.AddCapturedFlags();
        playerInfo.Stats.AddCapturedFlags();

        // Act
        playerRepository.UpdateCapturedFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.CapturedFlags.Should().Be(expectedCapturedFlags);
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
        playerInfo.Stats.AddDroppedFlags();
        playerInfo.Stats.AddDroppedFlags();

        // Act
        playerRepository.UpdateDroppedFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.DroppedFlags.Should().Be(expectedDroppedFlags);
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
        playerInfo.Stats.AddReturnedFlags();
        playerInfo.Stats.AddReturnedFlags();

        // Act
        playerRepository.UpdateReturnedFlags(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.ReturnedFlags.Should().Be(expectedReturnedFlags);
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
        playerInfo.Stats.AddHeadShots();
        playerInfo.Stats.AddHeadShots();

        // Act
        playerRepository.UpdateHeadShots(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.HeadShots.Should().Be(expectedHeadShots);
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
        playerInfo.Stats.AddGunGameWins();
        playerInfo.Stats.AddGunGameWins();

        // Act
        playerRepository.UpdateGunGameWins(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.GunGameWins.Should().Be(expectedGunGameWins);
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
        playerInfo.Role.Set(expectedRoleId);

        // Act
        playerRepository.UpdateRole(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Role.Id.Should().Be(expectedRoleId);
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
        playerInfo.Appearance.SetSkin(expectedSkinId);

        // Act
        playerRepository.UpdateSkin(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Appearance.SkinId.Should().Be(expectedSkinId);
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
        playerInfo.Stats.SetRank(expectedRankId);

        // Act
        playerRepository.UpdateRank(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.RankId.Should().Be(expectedRankId);
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
        playerInfo.Stats.SetLastConnection();

        // Act
        playerRepository.UpdateLastConnection(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerName);

        // Assert
        actual.Stats.LastConnection.Should().BeSameDateAs(playerInfo.Stats.LastConnection);
    }
}
