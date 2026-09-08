using CTF.Application.IVP;

namespace CTF.Application.Tests.Combat;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Combat</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
internal static class ChangeDrivers { }
