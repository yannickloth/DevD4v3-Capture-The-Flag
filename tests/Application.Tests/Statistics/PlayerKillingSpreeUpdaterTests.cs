namespace CTF.Application.Tests.Statistics;

/// <summary>Tests for PlayerKillingSpreeUpdater.HasSurpassedMaxKillingSpree.</summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: PlayerKillingSpreeUpdater.HasSurpassedMaxKillingSpree); CD-26 (NUnit test-framework contract) → CD-10; CD-27 (FluentAssertions contract) → CD-10</remarks>
public class PlayerKillingSpreeUpdaterTests
{
    [Test]
    public void HasSurpassedMaxKillingSpree_WhenNewRecordIsAchieved_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.StatsPerRound.AddKillingSpree();
        player.StatsPerRound.AddKillingSpree();
        player.StatsPerRound.AddKillingSpree();
        player.SetMaxKillingSpree(2);

        // Act
        bool actual = PlayerKillingSpreeUpdater.HasSurpassedMaxKillingSpree(player);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasSurpassedMaxKillingSpree_WhenNewRecordIsNotAchieved_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.StatsPerRound.AddKillingSpree();
        player.StatsPerRound.AddKillingSpree();
        player.SetMaxKillingSpree(3);

        // Act
        bool actual = PlayerKillingSpreeUpdater.HasSurpassedMaxKillingSpree(player);

        // Assert
        actual.Should().BeFalse();
    }
}
