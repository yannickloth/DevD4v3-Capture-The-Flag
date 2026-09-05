namespace CTF.Application.Tests.Statistics.PlayerStats;

/// <summary>Tests for PlayerKillingSpreeUpdater.HasSurpassedMaxKillingSpree.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class PlayerKillingSpreeUpdaterTests
{
    [Test]
    public void HasSurpassedMaxKillingSpree_WhenNewRecordIsAchieved_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Stats.PerRound.AddKillingSpree();
        player.Stats.PerRound.AddKillingSpree();
        player.Stats.PerRound.AddKillingSpree();
        player.Stats.SetMaxKillingSpree(2);

        // Act
        bool actual = PlayerKillingSpreeUpdater.HasSurpassedMaxKillingSpree(player);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasSurpassedMaxKillingSpree_WhenNewRecordIsNotAchieved_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Stats.PerRound.AddKillingSpree();
        player.Stats.PerRound.AddKillingSpree();
        player.Stats.SetMaxKillingSpree(3);

        // Act
        bool actual = PlayerKillingSpreeUpdater.HasSurpassedMaxKillingSpree(player);

        // Assert
        actual.Should().BeFalse();
    }
}
