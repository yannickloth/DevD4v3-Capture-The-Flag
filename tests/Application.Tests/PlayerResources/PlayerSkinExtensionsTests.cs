namespace CTF.Application.Tests.PlayerResources;

/// <summary>Tests for PlayerSkinExtensions.HasSkin.</summary>
[ChangeDriversAttribute(ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class PlayerSkinExtensionsTests
{
    [Test]
    public void HasSkin_WhenPlayerHasAssignedSkin_ShouldReturnTrue()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Appearance.SetSkin(311);

        // Act
        bool actual = player.HasSkin();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    [ChangeDriversAttribute(ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
    public void HasSkin_WhenPlayerHasNoAssignedSkin_ShouldReturnFalse()
    {
        // Arrange
        var player = new PlayerInfo();
        player.Appearance.RemoveSkin();

        // Act
        bool actual = player.HasSkin();

        // Assert
        actual.Should().BeFalse();
    }
}
