namespace CTF.Application.Tests.WeaponCatalogs.Catalogs.Definitions;

/// <summary>Tests for WeaponCatalogType.</summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
public class WeaponCatalogTypeTests
{
    [Test]
    public void AllValues_ShouldHaveDisplayName()
    {
        foreach (WeaponCatalogType type in Enum.GetValues<WeaponCatalogType>())
        {
            Action action = () => type.GetDisplayName();
            action.Should().NotThrow();
        }
    }
}
