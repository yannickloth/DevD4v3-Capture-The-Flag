namespace CTF.Application.Tests.Statistics;

/// <summary>Tests for PlayerStatsRenderer.GetStatsAsText.</summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: PlayerStatsRenderer.GetStatsAsText); CD-26 (NUnit test-framework contract) → CD-10; CD-27 (FluentAssertions contract) → CD-10</remarks>
public class PlayerStatsRendererTests
{
    [Test]
    public void GetStatsAsText_WhenStatsAreObtained_ShouldReturnValidStringFormat()
    {
        // Arrange
        var player = new PlayerInfo();
        int maxRank = RankCollection.Count;
        var expectedString =
            "~w~KILLS: ~y~0 ~w~DEATHS: ~y~0 ~w~SPREE: ~y~0 " +
            $"~w~COINS: ~y~0/100 ~w~LEVEL: ~y~1/{maxRank} ~w~RANK: ~y~Noob";

        // Act
        string actual = PlayerStatsRenderer.GetStatsAsText(player);

        // Assert
        actual.Should().Be(expectedString);
    }
}
