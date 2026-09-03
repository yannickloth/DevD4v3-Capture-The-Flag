namespace CTF.Application.Tests.GameRules;

/// <summary>Tests for Flag.IsCarriedBy.</summary>/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: Flag.IsCarriedBy); CD-26 (NUnit test-framework contract) → CD-02; CD-27 (FluentAssertions contract) → CD-02</remarks>
public class IsCarryingEnemyFlagTests
{
    [SetUp]
    public void Init()
    {
        Team.Alpha.Reset();
        Team.Beta.Reset();
    }

    [Test]
    public void IsCarriedBy_WhenPlayerIsNotAssignedToAnyTeam_ShouldReturnFalse()
    {
        // Arrange
        var fakePlayer = new FakePlayer(id: 1, name: "Bob", team: TeamId.NoTeam);
        var player = new PlayerInfo();
        player.SetTeam(TeamId.NoTeam);
        player.SetName(fakePlayer.Name);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(fakePlayer);

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase("Bob")]
    [TestCase("BOB")]
    [TestCase("bob")]
    public void IsCarriedBy_WhenPlayerFromTheAlphaTeamIsCarryingTheBetaFlag_ShouldReturnTrue(string playerName)
    {
        // Arrange
        Team betaTeam = Team.Beta;
        var alphaTeamPlayer = new FakePlayer(id: 1, playerName, team: TeamId.Alpha);
        var player = new PlayerInfo();
        player.SetTeam(TeamId.Alpha);
        player.SetName("Bob");
        betaTeam.Flag.Capture(alphaTeamPlayer);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(alphaTeamPlayer);

        // Assert
        actual.Should().BeTrue();
    }

    [TestCase("Bob")]
    [TestCase("BOB")]
    [TestCase("bob")]
    public void IsCarriedBy_WhenPlayerFromTheBetaTeamIsCarryingTheAlphaFlag_ShouldReturnTrue(string playerName)
    {
        // Arrange
        Team alphaTeam = Team.Alpha;
        var betaTeamPlayer = new FakePlayer(id: 1, playerName, team: TeamId.Beta);
        var player = new PlayerInfo();
        player.SetTeam(TeamId.Beta);
        player.SetName("Bob");
        alphaTeam.Flag.Capture(betaTeamPlayer);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(betaTeamPlayer);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsCarriedBy_WhenAnotherPlayerFromTheAlphaTeamIsCarryingTheBetaFlag_ShouldReturnFalse()
    {
        // Arrange
        Team betaTeam = Team.Beta;
        var alphaTeamPlayer1 = new FakePlayer(id: 1, name: "Bob", team: TeamId.Alpha);
        var alphaTeamPlayer2 = new FakePlayer(id: 2, name: "Alice", team: TeamId.Alpha);
        var player = new PlayerInfo();
        player.SetTeam(TeamId.Alpha);
        player.SetName(alphaTeamPlayer1.Name);
        betaTeam.Flag.Capture(alphaTeamPlayer2);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(alphaTeamPlayer1);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsCarriedBy_WhenAnotherPlayerFromTheBetaTeamIsCarryingTheAlphaFlag_ShouldReturnFalse()
    {
        // Arrange
        Team alphaTeam = Team.Alpha;
        var betaTeamPlayer1 = new FakePlayer(id: 1, name: "Bob", team: TeamId.Beta);
        var betaTeamPlayer2 = new FakePlayer(id: 2, name: "Alice", team: TeamId.Beta);
        var player = new PlayerInfo();
        player.SetTeam(TeamId.Beta);
        player.SetName(betaTeamPlayer1.Name);
        alphaTeam.Flag.Capture(betaTeamPlayer2);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(betaTeamPlayer1);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsCarriedBy_WhenPlayerFromTheAlphaTeamTakesADroppedBetaFlag_ShouldReturnTrue()
    {
        // Arrange
        Team betaTeam = Team.Beta;
        var alphaTeamPlayer = new FakePlayer(
            id: 1,
            name: "Bob",
            team: TeamId.Alpha);

        var player = new PlayerInfo();
        player.SetTeam(TeamId.Alpha);
        player.SetName("Bob");
        betaTeam.Flag.Drop();
        betaTeam.Flag.Take(alphaTeamPlayer);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(alphaTeamPlayer);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsCarriedBy_WhenPlayerFromTheBetaTeamTakesADroppedAlphaFlag_ShouldReturnTrue()
    {
        // Arrange
        Team alphaTeam = Team.Alpha;
        var betaTeamPlayer = new FakePlayer(
            id: 1,
            name: "Bob",
            team: TeamId.Beta);

        var player = new PlayerInfo();
        player.SetTeam(TeamId.Beta);
        player.SetName("Bob");
        alphaTeam.Flag.Drop();
        alphaTeam.Flag.Take(betaTeamPlayer);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(betaTeamPlayer);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsCarriedBy_WhenTheAlphaFlagHasNoCarrier_ShouldReturnFalse()
    {
        // Arrange
        var betaTeamPlayer = new FakePlayer(id: 1, name: "Bob", team: TeamId.Beta);
        var player = new PlayerInfo();
        player.SetTeam(TeamId.Beta);
        player.SetName(betaTeamPlayer.Name);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(betaTeamPlayer);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsCarriedBy_WhenTheBetaFlagHasNoCarrier_ShouldReturnFalse()
    {
        // Arrange
        var alphaTeamPlayer = new FakePlayer(id: 1, name: "Bob", team: TeamId.Alpha);
        var player = new PlayerInfo();
        player.SetTeam(TeamId.Alpha);
        player.SetName(alphaTeamPlayer.Name);

        // Act
        bool actual = player.Team.RivalTeam.Flag.IsCarriedBy(alphaTeamPlayer);

        // Assert
        actual.Should().BeFalse();
    }
}
