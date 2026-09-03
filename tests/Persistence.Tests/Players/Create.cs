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
        playerInfo.Account.SetName("Player1");
        playerInfo.Account.SetPassword("DSR8887$#");
        playerInfo.Stats.SetTotalKills(10);
        playerInfo.Stats.SetTotalDeaths(10);
        playerInfo.Stats.SetMaxKillingSpree(5);
        playerInfo.Appearance.SetSkin(146);
        playerInfo.Stats.AddBroughtFlags();
        playerInfo.Stats.AddCapturedFlags();
        playerInfo.Stats.AddDroppedFlags();
        playerInfo.Stats.AddReturnedFlags();
        playerInfo.Stats.AddHeadShots();
        playerInfo.Stats.AddGunGameWins();

        // Act
        playerRepository.Create(playerInfo);
        PlayerInfo actual = playerRepository.GetOrDefault(playerInfo.Account.Name);

        // Asserts
        actual.Account.AccountId.Should().BeGreaterThan(0);
        actual.Account.Name.Should().Be(playerInfo.Account.Name);
        actual.Account.Password.Should().Be(playerInfo.Account.Password);
        actual.Role.Id.Should().Be(RoleId.Basic);
        actual.Stats.RankId.Should().Be(RankId.Noob);
        actual.Stats.TotalKills.Should().Be(playerInfo.Stats.TotalKills);
        actual.Stats.TotalDeaths.Should().Be(playerInfo.Stats.TotalDeaths);
        actual.Stats.MaxKillingSpree.Should().Be(playerInfo.Stats.MaxKillingSpree);
        actual.Appearance.SkinId.Should().Be(playerInfo.Appearance.SkinId);
        actual.Stats.BroughtFlags.Should().Be(playerInfo.Stats.BroughtFlags);
        actual.Stats.CapturedFlags.Should().Be(playerInfo.Stats.CapturedFlags);
        actual.Stats.DroppedFlags.Should().Be(playerInfo.Stats.DroppedFlags);
        actual.Stats.ReturnedFlags.Should().Be(playerInfo.Stats.ReturnedFlags);
        actual.Stats.HeadShots.Should().Be(playerInfo.Stats.HeadShots);
        actual.Stats.GunGameWins.Should().Be(playerInfo.Stats.GunGameWins);
    }
}
