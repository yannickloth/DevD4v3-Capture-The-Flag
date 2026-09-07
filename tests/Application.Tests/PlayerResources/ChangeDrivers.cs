using CTF.Application.IVP;

namespace CTF.Application.Tests.PlayerResources;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.PlayerResources</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Model, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
internal static class ChangeDrivers { }
