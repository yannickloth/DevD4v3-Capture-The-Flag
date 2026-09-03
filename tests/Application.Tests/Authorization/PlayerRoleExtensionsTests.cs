namespace CTF.Application.Tests.Authorization;

/// <summary>Tests for PlayerRoleExtensions.</summary>
/// <remarks>Change drivers: CD-09 (root; authorization policy: PlayerRoleExtensions); CD-26 (NUnit test-framework contract) → CD-09; CD-27 (FluentAssertions contract) → CD-09</remarks>
public class PlayerRoleExtensionsTests
{
    [Test]
    public void HasRole_WhenRoleIsAdmin_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        RoleId roleId = RoleId.Admin;
        player.SetRole(roleId);

        // Act
        bool actual = player.HasRole(roleId);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasRole_WhenRoleIsNotAdmin_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Admin);

        // Act
        bool actual = player.HasRole(RoleId.Basic);

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase(RoleId.Basic)]
    [TestCase(RoleId.VIP)]
    [TestCase(RoleId.Moderator)]
    public void HasLowerRoleThan_WhenPlayerHasLowerRoleThanAdmin_ShouldReturnTrue(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Admin);

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasLowerRoleThan_WhenPlayerHasNoLowerRoleThanAdmin_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Admin);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Admin);

        // Assert
        actual.Should().BeFalse();
    }

    [TestCase(RoleId.Basic)]
    [TestCase(RoleId.VIP)]
    public void HasLowerRoleThan_WhenPlayerHasLowerRoleThanModerator_ShouldReturnTrue(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Moderator);

        // Assert
        actual.Should().BeTrue();
    }

    [TestCase(RoleId.Moderator)]
    [TestCase(RoleId.Admin)]
    public void HasLowerRoleThan_WhenPlayerHasNoLowerRoleThanModerator_ShouldReturnFalse(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.Moderator);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void HasLowerRoleThan_WhenPlayerHasLowerRoleThanVIP_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Basic);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.VIP);

        // Assert
        actual.Should().BeTrue();
    }

    [TestCase(RoleId.VIP)]
    [TestCase(RoleId.Moderator)]
    [TestCase(RoleId.Admin)]
    public void HasLowerRoleThan_WhenPlayerHasNoLowerRoleThanVIP_ShouldReturnFalse(RoleId roleId)
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(roleId);

        // Act
        bool actual = player.HasLowerRoleThan(RoleId.VIP);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsVIP_WhenPlayerIsVIP_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.VIP);

        // Act
        bool actual = player.IsVIP();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsVIP_WhenPlayerIsNotVIP_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Basic);

        // Act
        bool actual = player.IsVIP();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsModerator_WhenPlayerIsModerator_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Moderator);

        // Act
        bool actual = player.IsModerator();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsModerator_WhenPlayerIsNotModerator_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Basic);

        // Act
        bool actual = player.IsModerator();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void IsAdmin_WhenPlayerIsAdmin_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Admin);

        // Act
        bool actual = player.IsAdmin();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void IsAdmin_WhenPlayerIsNotAdmin_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.SetRole(RoleId.Basic);

        // Act
        bool actual = player.IsAdmin();

        // Assert
        actual.Should().BeFalse();
    }
}
