namespace CTF.Application.Tests.Players.Accounts;

/// <summary>Tests for PlayerInfo.SetRole.</summary>
/// <remarks>Change drivers: CD-09 (root; authorization policy: PlayerInfo.SetRole); CD-26 (NUnit test-framework contract) → CD-09; CD-27 (FluentAssertions contract) → CD-09</remarks>
public class PlayerInfoRoleTests
{
    static readonly int[] InvalidRoleCases = [-1, -2, RoleCollection.Count];

    [TestCaseSource(nameof(InvalidRoleCases))]
    public void SetRole_WhenRoleIdIsInvalid_ShouldReturnFailureResult(int value)
    {
        // Arrange
        var player = new PlayerInfo();
        RoleId roleId = (RoleId)value;
        var expectedMessage = Messages.InvalidRole;

        // Act
        Result result = player.SetRole(roleId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.RoleId.Should().NotBe(roleId);
    }

    [Test]
    public void SetRole_WhenRoleIdIsValid_ShouldReturnSuccessResult()
    {
        // Arrange
        var player = new PlayerInfo();
        RoleId roleId = RoleId.Admin;

        // Act
        Result result = player.SetRole(roleId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.RoleId.Should().Be(roleId);
    }
}
