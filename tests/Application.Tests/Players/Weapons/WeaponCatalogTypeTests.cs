namespace CTF.Application.Tests.Players.Weapons;

/// <summary>Tests for WeaponCatalogType.</summary>
/// <remarks>Change drivers: CD-26 (NUnit test-framework contract), CD-27 (FluentAssertions contract), CD-29 (code-under-test: WeaponCatalogType), CD-04 (weapon-catalog configuration)</remarks>
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
