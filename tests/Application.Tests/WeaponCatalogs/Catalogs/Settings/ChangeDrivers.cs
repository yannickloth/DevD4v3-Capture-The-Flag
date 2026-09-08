
namespace CTF.Application.Tests.WeaponCatalog.Configuration;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.WeaponCatalogs.Configuration</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
