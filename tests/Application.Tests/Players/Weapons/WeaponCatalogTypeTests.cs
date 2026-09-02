namespace CTF.Application.Tests.Players.Weapons;

/// <summary>Tests for WeaponCatalogType.</summary>
/// <remarks>Change drivers: CD-29 (root; code-under-test: WeaponCatalogType); CD-26 (NUnit test-framework contract) → CD-29; CD-27 (FluentAssertions contract) → CD-29; CD-04 (weapon-catalog configuration) → CD-29</remarks>
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
