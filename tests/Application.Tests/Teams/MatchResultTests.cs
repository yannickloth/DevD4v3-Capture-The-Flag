namespace CTF.Application.Tests.Teams;

/// <summary>Tests for MatchResult.</summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification); CD-29 (code-under-test: MatchResult); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29</remarks>
public class MatchResultTests
{
    [SetUp]
    public void Init()
    {
        Team.Alpha.Reset();
        Team.Beta.Reset();
    }

    [Test]
    public void Create_WhenAlphaTeamWins_ShouldReturnAlphaAsWinner()
    {
        // Arrange
        Team.Alpha.StatsPerRound.AddScore();

        // Act
        MatchResult result = MatchResult.Create(Team.Alpha, Team.Beta);

        // Assert
        result.Winner.Should().Be(Team.Alpha);
        result.IsTie.Should().BeFalse();
    }

    [Test]
    public void Create_WhenBetaTeamWins_ShouldReturnBetaAsWinner()
    {
        // Arrange
        Team.Beta.StatsPerRound.AddScore();

        // Act
        MatchResult result = MatchResult.Create(Team.Alpha, Team.Beta);

        // Assert
        result.Winner.Should().Be(Team.Beta);
        result.IsTie.Should().BeFalse();
    }

    [Test]
    public void Create_WhenNoTeamWins_ShouldReturnTieResult()
    {
        // Arrange
        Team.Alpha.StatsPerRound.AddScore();
        Team.Beta.StatsPerRound.AddScore();

        // Act
        MatchResult result = MatchResult.Create(Team.Alpha, Team.Beta);

        // Assert
        result.Winner.Should().Be(Team.None);
        result.IsTie.Should().BeTrue();
    }
}
