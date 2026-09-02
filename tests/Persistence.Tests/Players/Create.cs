namespace Persistence.Tests.Players;

/// <summary>Verifies the repository Create operation sets the account id and persists all fields.</summary>
/// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.Create); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
public class CreatePlayer
{
    /// <summary>Creates a player and asserts every field is persisted and the id is generated.</summary>
    /// <remarks>Change drivers: CD-20 (root; outbound repository contract: IPlayerRepository.Create); CD-26 (NUnit test-framework contract) → CD-20; CD-27 (FluentAssertions contract) → CD-20; CD-18 (database schema/player data model) → CD-20</remarks>
    [TestCaseSource(typeof(RepositoryManagerTestCases))]
    public void Create_WhenCalled_ShouldCreatePlayerAndSetAccountId(DatabaseProvider provider)
    {
        // Arrange
        using IRepositoryManager repositoryManager = RepositoryManagerFactory.Create(provider);
        repositoryManager.InitializeSeedData();
        IPlayerRepository playerRepository = repositoryManager.PlayerRepository;
        var playerInfo = new PlayerInfo();
        playerInfo.SetName("Player1");
        playerInfo.SetPassword("DSR8887$#");
        playerInfo.SetTotalKills(10);
        playerInfo.SetTotalDeaths(10);
        playerInfo.SetMaxKillingSpree(5);
        playerInfo.SetSkin(146);
        playerInfo.AddBroughtFlags();
        playerInfo.AddCapturedFlags();
        playerInfo.AddDroppedFlags();
        playerInfo.AddReturnedFlags();
        playerInfo.AddHeadShots();
        playerInfo.AddGunGameWins();

        // Act
        playerRepository.Create(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerInfo.Name);

        // Asserts
        actual.AccountId.Should().BeGreaterThan(0);
        actual.Name.Should().Be(playerInfo.Name);
        actual.Password.Should().Be(playerInfo.Password);
        actual.RoleId.Should().Be(RoleId.Basic);
        actual.RankId.Should().Be(RankId.Noob);
        actual.TotalKills.Should().Be(playerInfo.TotalKills);
        actual.TotalDeaths.Should().Be(playerInfo.TotalDeaths);
        actual.MaxKillingSpree.Should().Be(playerInfo.MaxKillingSpree);
        actual.SkinId.Should().Be(playerInfo.SkinId);
        actual.BroughtFlags.Should().Be(playerInfo.BroughtFlags);
        actual.CapturedFlags.Should().Be(playerInfo.CapturedFlags);
        actual.DroppedFlags.Should().Be(playerInfo.DroppedFlags);
        actual.ReturnedFlags.Should().Be(playerInfo.ReturnedFlags);
        actual.HeadShots.Should().Be(playerInfo.HeadShots);
        actual.GunGameWins.Should().Be(playerInfo.GunGameWins);
    }
}
