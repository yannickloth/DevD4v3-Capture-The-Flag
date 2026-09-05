namespace CTF.Application.Tests.Authorization;

/// <summary>Tests for PlayerRoleExtensions.</summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class PlayerRoleExtensionsTests
{
    [Test]
    public void HasRole_WhenRoleIsAdmin_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        RoleId roleId = RoleId.Admin;
        player.Role.Set(roleId);

        // Act
        bool actual = player.HasRole(roleId);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasRole_WhenRoleIsNotAdmin_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Admin);

        // Act
        bool actual = player.HasRole(RoleId.Basic);

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase(RoleId.Basic)]
    [TestCase(RoleId.VIP)]
    [TestCase(RoleId.Moderator)]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasLowerRoleThan_WhenPlayerHasLowerRoleThanAdmin_ShouldReturnTrue(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Admin);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasLowerRoleThan_WhenPlayerHasNoLowerRoleThanAdmin_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Admin);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Admin);

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase(RoleId.Basic)]
    [TestCase(RoleId.VIP)]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasLowerRoleThan_WhenPlayerHasLowerRoleThanModerator_ShouldReturnTrue(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Moderator);

        // Assert
        actual.Should().BeTrue();
    }

    [TestCase(RoleId.Moderator)]
    [TestCase(RoleId.Admin)]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasLowerRoleThan_WhenPlayerHasNoLowerRoleThanModerator_ShouldReturnFalse(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Moderator);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasLowerRoleThan_WhenPlayerHasLowerRoleThanVIP_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Basic);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.VIP);

        // Assert
        actual.Should().BeTrue();
    }

    [TestCase(RoleId.VIP)]
    [TestCase(RoleId.Moderator)]
    [TestCase(RoleId.Admin)]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasLowerRoleThan_WhenPlayerHasNoLowerRoleThanVIP_ShouldReturnFalse(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.VIP);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsVIP_WhenPlayerIsVIP_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.VIP);

        // Act
        bool actual = player.IsVIP();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsVIP_WhenPlayerIsNotVIP_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Basic);

        // Act
        bool actual = player.IsVIP();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsModerator_WhenPlayerIsModerator_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Moderator);

        // Act
        bool actual = player.IsModerator();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsModerator_WhenPlayerIsNotModerator_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Basic);

        // Act
        bool actual = player.IsModerator();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsAdmin_WhenPlayerIsAdmin_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Admin);

        // Act
        bool actual = player.IsAdmin();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void IsAdmin_WhenPlayerIsNotAdmin_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Role.Set(RoleId.Basic);

        // Act
        bool actual = player.IsAdmin();

        // Assert
        actual.Should().BeFalse();
    }
}
