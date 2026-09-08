
namespace CTF.Application.WeaponCatalogs.ConfigurationDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.WeaponCatalogs.ActiveCatalog</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
