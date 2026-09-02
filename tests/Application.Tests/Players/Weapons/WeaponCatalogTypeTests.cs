namespace CTF.Application.Tests.Players.Weapons;

/// <summary>Tests for WeaponCatalogType.</summary>
/// <remarks>Change drivers: CD-04 (root; weapon-catalog configuration: WeaponCatalogType); CD-26 (NUnit test-framework contract) → CD-04; CD-27 (FluentAssertions contract) → CD-04</remarks>
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
