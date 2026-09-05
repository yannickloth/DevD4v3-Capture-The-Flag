namespace CTF.Application.Tests.GunGames.Gamma_CD07_CD26_CD27;

/// <summary>Tests for MaxWeaponLevel.</summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules: MaxWeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
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
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: MaxWeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
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
