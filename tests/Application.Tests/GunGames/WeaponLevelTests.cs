namespace CTF.Application.Tests.GunGames;

/// <summary>Tests for WeaponLevel.</summary>
/// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
public class WeaponLevelTests
{
    private readonly MaxWeaponLevel _maxLevel = new(4);

    [Test]
    public void Next_WhenWeaponLevelIsBelowMaxLevel_ShouldAdvanceToNextLevel()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;

        // Act
        WeaponLevel secondLevel = firstLevel.Next(_maxLevel);

        // Assert
        secondLevel.Value.Should().Be(2);
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void Next_WhenWeaponLevelIsAtMaxLevel_ShouldRemainAtMaxLevel()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;
        WeaponLevel secondLevel = firstLevel.Next(_maxLevel);
        WeaponLevel thirdLevel = secondLevel.Next(_maxLevel);
        WeaponLevel finalLevel = thirdLevel.Next(_maxLevel);

        // Act
        WeaponLevel nextLevel = finalLevel.Next(_maxLevel);

        // Assert
        nextLevel.Value.Should().Be(_maxLevel.Value);
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void Previous_WhenWeaponLevelIsAboveFirstLevel_ShouldMoveToPreviousLevel()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;
        WeaponLevel secondLevel = firstLevel.Next(_maxLevel);
        WeaponLevel thirdLevel = secondLevel.Next(_maxLevel);

        // Act
        WeaponLevel previousLevel = thirdLevel.Previous();

        // Assert
        previousLevel.Should().Be(secondLevel);
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void Previous_WhenWeaponLevelIsAtFirstLevel_ShouldRemainAtFirstLevel()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;

        // Act
        WeaponLevel previousLevel = firstLevel.Previous();

        // Assert
        previousLevel.Should().Be(firstLevel);
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void IsMax_WhenWeaponLevelIsMaxLevel_ShouldReturnTrue()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;
        WeaponLevel secondLevel = firstLevel.Next(_maxLevel);
        WeaponLevel thirdLevel = secondLevel.Next(_maxLevel);
        WeaponLevel finalLevel = thirdLevel.Next(_maxLevel);

        // Act
        bool isMax = finalLevel.IsMax(_maxLevel);

        // Assert
        isMax.Should().BeTrue();
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void IsMax_WhenWeaponLevelIsBelowMaxLevel_ShouldReturnFalse()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;
        WeaponLevel secondLevel = firstLevel.Next(_maxLevel);

        // Act
        bool isMax = secondLevel.IsMax(_maxLevel);

        // Assert
        isMax.Should().BeFalse();
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void ToString_ShouldReturnWeaponLevelValue()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;
        WeaponLevel secondLevel = firstLevel.Next(_maxLevel);

        // Act
        string value = secondLevel.ToString();

        // Assert
        value.Should().Be("2");
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void EqualityOperator_WhenWeaponLevelsAreEqual_ShouldReturnTrue()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;
        WeaponLevel left = firstLevel.Next(_maxLevel);
        WeaponLevel right = firstLevel.Next(_maxLevel);

        // Act
        bool areEqual = left == right;

        // Assert
        areEqual.Should().BeTrue();
    }

    [Test]
    /// <remarks>Change drivers: CD-07 (root; GunGame mode rules: WeaponLevel); CD-26 (NUnit test-framework contract) → CD-07; CD-27 (FluentAssertions contract) → CD-07</remarks>
    public void GreaterThanOperator_WhenLeftWeaponLevelIsGreater_ShouldReturnTrue()
    {
        // Arrange
        WeaponLevel firstLevel = WeaponLevel.First;
        WeaponLevel secondLevel = firstLevel.Next(_maxLevel);

        // Act
        bool isGreater = secondLevel > firstLevel;

        // Assert
        isGreater.Should().BeTrue();
    }
}
