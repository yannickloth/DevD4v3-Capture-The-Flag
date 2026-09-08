namespace CTF.Application.Tests.Statistics;

/// <summary>Tests for PlayerStatsPerRound.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class PlayerStatsPerRoundTests
{
    [Test]
    public void AddKills_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var stats = new PlayerStatsPerRound();
        int expectedKills = 2;

        // Act
        stats.AddKills();
        stats.AddKills();

        // Assert
        stats.Kills.Should().Be(expectedKills);
    }

    [Test]
    public void AddDeaths_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var stats = new PlayerStatsPerRound();
        int expectedDeaths = 2;

        // Act
        stats.AddDeaths();
        stats.AddDeaths();

        // Assert
        stats.Deaths.Should().Be(expectedDeaths);
    }

    [Test]
    public void AddKillingSpree_WhenCalledTwice_ShouldBeIncreasedTo2()
    {
        // Arrange
        var stats = new PlayerStatsPerRound();
        int expectedKillingSpree = 2;

        // Act
        stats.AddKillingSpree();
        stats.AddKillingSpree();

        // Assert
        stats.KillingSpree.Should().Be(expectedKillingSpree);
    }

    [Test]
    public void ResetStats_WhenCalled_ShouldResetStatsToZero()
    {
        // Arrange
        var stats = new PlayerStatsPerRound();
        stats.AddKills();
        stats.AddKills();
        stats.AddDeaths();
        stats.AddDeaths();
        stats.AddKillingSpree();
        stats.AddKillingSpree();

        // Act
        stats.ResetStats();

        // Asserts
        stats.Kills.Should().Be(0);
        stats.Deaths.Should().Be(0);
        stats.KillingSpree.Should().Be(0);
    }
}
