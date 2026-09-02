namespace CTF.Application.Tests.Players.Weapons;

/// <summary>Tests for WeaponCatalogSettings.</summary>
/// <remarks>Change drivers: CD-26 (NUnit test-framework contract), CD-27 (FluentAssertions contract), CD-29 (code-under-test: WeaponCatalogSettings), CD-04 (weapon-catalog configuration), CD-17 (game configuration/.env schema)</remarks>
public class WeaponCatalogSettingsTests
{
    [Test]
    public void Constructor_WhenCatalogTypeIsInvalid_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        WeaponCatalogType type = (WeaponCatalogType)(-1);

        // Act
        Action act = () => new WeaponCatalogSettings(type);

        // Assert
        act.Should()
           .Throw<ArgumentOutOfRangeException>()
           .WithParameterName(nameof(type));
    }

    [Test]
    public void Constructor_WhenCatalogTypeIsValid_ShouldCreateInstance()
    {
        // Arrange
        WeaponCatalogType type = WeaponCatalogType.Run;

        // Act
        var settings = new WeaponCatalogSettings(type);

        // Assert
        settings.Type.Should().Be(type);
    }

    [Test]
    public void Change_WhenCatalogTypeIsInvalid_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        var settings = new WeaponCatalogSettings();
        WeaponCatalogType type = (WeaponCatalogType)(-1);

        // Act
        Action act = () => settings.Change(type);

        // Assert
        act.Should()
           .Throw<ArgumentOutOfRangeException>()
           .WithParameterName(nameof(type));
    }

    [Test]
    public void Change_WhenCatalogTypeIsValid_ShouldUpdateCatalog()
    {
        // Arrange
        var settings = new WeaponCatalogSettings(WeaponCatalogType.Walking);

        // Act
        settings.Change(WeaponCatalogType.Run);

        // Assert
        settings.Type.Should().Be(WeaponCatalogType.Run);
    }
}
