namespace CTF.Application.Tests.Platform;

/// <summary>Tests for TeamTextDrawRenderer.GetScoreAsText.</summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: TeamTextDrawRenderer.GetScoreAsText); CD-26 (NUnit test-framework contract) → CD-10; CD-27 (FluentAssertions contract) → CD-10</remarks>
public class TeamTextDrawRendererTests
{
    [SetUp]
    public void Init()
    {
        Team.Alpha.Reset();
        Team.Beta.Reset();
    }

    [Test]
    public void GetScoreAsText_WhenScoreIsObtained_ShouldReturnValidStringFormat()
    {
        // Arrange
        Team alphaTeam = Team.Alpha;
        alphaTeam.StatsPerRound.AddScore();
        var expectedString = "Alpha: 1";

        // Act
        string actual = TeamTextDrawRenderer.GetScoreAsText(alphaTeam);

        // Assert
        actual.Should().Be(expectedString);
    }
}
