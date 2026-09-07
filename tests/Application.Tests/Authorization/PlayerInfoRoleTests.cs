namespace CTF.Application.Tests.Authorization;

/// <summary>Tests for PlayerInfo.SetRole.</summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
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
        Result result = player.Role.Set(roleId);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be(expectedMessage);
        player.Role.Id.Should().NotBe(roleId);
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void SetRole_WhenRoleIdIsValid_ShouldReturnSuccessResult()
    {
        // Arrange
        var player = new PlayerInfo();
        RoleId roleId = RoleId.Admin;

        // Act
        Result result = player.Role.Set(roleId);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        player.Role.Id.Should().Be(roleId);
    }
}
