namespace CTF.Application.Tests.Statistics;

/// <summary>Tests for PlayerRankExtensions.HasRank.</summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: PlayerRankExtensions.HasRank); CD-26 (NUnit test-framework contract) → CD-10; CD-27 (FluentAssertions contract) → CD-10</remarks>
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
