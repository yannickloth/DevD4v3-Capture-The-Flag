namespace CTF.Application.Tests.Teams.Statistics;

/// <summary>Tests for TeamScoreFormatter.GetScoreAsText.</summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: TeamScoreFormatter.GetScoreAsText); CD-26 (NUnit test-framework contract) → CD-10; CD-27 (FluentAssertions contract) → CD-10</remarks>
public class TeamScoreFormatterTests
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
        string actual = alphaTeam.GetScoreAsText();

        // Assert
        actual.Should().Be(expectedString);
    }
}
