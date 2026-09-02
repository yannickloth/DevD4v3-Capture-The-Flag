namespace CTF.Application.Tests.Players.Accounts;

/// <summary>Tests for PlayerInfo.SetTeam.</summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: PlayerInfo.SetTeam); CD-26 (NUnit test-framework contract) → CD-02; CD-27 (FluentAssertions contract) → CD-02</remarks>
public class SelectedTeamTests
{
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(254)]
    [TestCase(256)]
    public void SetTeam_WhenTeamIsInvalid_ShouldReturnFailureResult(int id)
    {
        // Arrange
        var player = new PlayerInfo();
        TeamId teamId = (TeamId)id;
        var expectedMessage = Messages.InvalidTeam;

        // Act
        Result result = player.SetTeam(teamId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Team.Id.Should().Be(TeamId.NoTeam);
    }

    [TestCase(TeamId.Alpha)]
    [TestCase(TeamId.Beta)]
    [TestCase(TeamId.NoTeam)]
    public void SetTeam_WhenTeamIsValid_ShouldReturnSuccessResult(TeamId teamId)
    {
        // Arrange
        var player = new PlayerInfo();

        // Act
        Result result = player.SetTeam(teamId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Team.Id.Should().Be(teamId);
    }
}
