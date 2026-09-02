namespace CTF.Application.Tests.GunGames;

/// <summary>Tests for KillsRequiredPerLevel.</summary>
/// <remarks>Change drivers: CD-07 (GunGame mode rules); CD-29 (code-under-test: KillsRequiredPerLevel); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29</remarks>
public class KillsRequiredPerLevelTests
{
    [Test]
    public void Constructor_WhenValueIsGreaterThanZero_ShouldCreateKillsRequiredPerLevel()
    {
        // Arrange
        const int expectedValue = 2;

        // Act
        var killsRequired = new KillsRequiredPerLevel(expectedValue);

        // Assert
        killsRequired.Value.Should().Be(expectedValue);
    }

    [Test]
    public void Constructor_WhenValueIsLessThanOne_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        const int invalidValue = 0;

        // Act
        Action act = () => _ = new KillsRequiredPerLevel(invalidValue);

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
