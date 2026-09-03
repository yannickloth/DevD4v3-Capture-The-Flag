namespace CTF.Application.Tests.GameRules;

/// <summary>Tests for MatchResult.</summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: MatchResult); CD-26 (NUnit test-framework contract) → CD-02; CD-27 (FluentAssertions contract) → CD-02</remarks>
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
