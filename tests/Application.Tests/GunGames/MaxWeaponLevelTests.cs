namespace CTF.Application.Tests.GunGames;

/// <summary>Tests for MaxWeaponLevel.</summary>
/// <remarks>Change drivers: CD-07 (GunGame mode rules), CD-29 (code-under-test: MaxWeaponLevel), CD-26 (NUnit test-framework contract), CD-27 (FluentAssertions contract)</remarks>
public class MaxWeaponLevelTests
{
    [Test]
    public void Constructor_WhenValueIsGreaterThanZero_ShouldCreateMaxWeaponLevel()
    {
        // Arrange
        const int expectedValue = 5;

        // Act
        var maxWeaponLevel = new MaxWeaponLevel(expectedValue);

        // Assert
        maxWeaponLevel.Value.Should().Be(expectedValue);
    }

    [Test]
    public void Constructor_WhenValueIsLessThanOne_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        const int invalidValue = 0;

        // Act
        Action act = () => _ = new MaxWeaponLevel(invalidValue);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
