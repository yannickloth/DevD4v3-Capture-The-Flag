namespace CTF.Application.Tests.WeaponCatalogs.Catalogs.Settings;

/// <summary>Tests for WeaponCatalogSettings.</summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Configuration)]
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
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Configuration)]
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
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Configuration)]
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
    [ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Configuration)]
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
