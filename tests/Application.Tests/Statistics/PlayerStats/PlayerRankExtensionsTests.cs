namespace CTF.Application.Tests.Statistics.PlayerStats;

/// <summary>Tests for PlayerRankExtensions.HasRank.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class PlayerRankExtensionsTests
{
    [Test]
    public void HasRank_WhenRankIsNoob_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        RankId rankId = RankId.Noob;
        player.Stats.SetRank(rankId);

        // Act
        bool actual = player.HasRank(rankId);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasRank_WhenRankIsNotNoob_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Stats.SetRank(RankId.Noob);

        // Act
        bool actual = player.HasRank(RankId.Junior);

        // Assert
        actual.Should().BeFalse();
    }
}
