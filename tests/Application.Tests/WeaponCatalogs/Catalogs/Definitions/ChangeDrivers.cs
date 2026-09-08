using CTF.Application.IVP;

namespace CTF.Application.Tests.WeaponCatalog;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.WeaponCatalog</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
internal static class ChangeDrivers { }
