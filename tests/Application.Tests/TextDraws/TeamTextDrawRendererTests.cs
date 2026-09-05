namespace CTF.Application.Tests.TextDraws;

/// <summary>Tests for TeamTextDrawRenderer.GetScoreAsText.</summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class TeamTextDrawRendererTests
{
    [SetUp]
    public void Init()
    {
        Team.Alpha.Reset();
        Team.Beta.Reset();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
