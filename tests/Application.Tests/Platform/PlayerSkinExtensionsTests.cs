namespace CTF.Application.Tests.Platform;

/// <summary>Tests for PlayerSkinExtensions.HasSkin.</summary>
/// <remarks>Change drivers: CD-44 (root; skin id resources); CD-26 (NUnit test-framework contract) → CD-44; CD-27 (FluentAssertions contract) → CD-44</remarks>
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
