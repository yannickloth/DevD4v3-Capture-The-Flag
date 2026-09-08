namespace CTF.Application.Tests.GameRules;

/// <summary>Tests for PlayerInfo.SetTeam.</summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class SelectedTeamTests
{
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(254)]
    [TestCase(256)]
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetTeam_WhenTeamIsInvalid_ShouldReturnFailureResult(int id)
    {
        // Arrange
        var player = new PlayerInfo();
        TeamId teamId = (TeamId)id;
        var expectedMessage = Messages.InvalidTeam;

        // Act
        Result result = player.Appearance.SetTeam(teamId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Appearance.Team.Id.Should().Be(TeamId.NoTeam);
    }

    [TestCase(TeamId.Alpha)]
    [TestCase(TeamId.Beta)]
    [TestCase(TeamId.NoTeam)]
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetTeam_WhenTeamIsValid_ShouldReturnSuccessResult(TeamId teamId)
    {
        // Arrange
        var player = new PlayerInfo();

        // Act
        Result result = player.Appearance.SetTeam(teamId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Appearance.Team.Id.Should().Be(teamId);
    }
}
