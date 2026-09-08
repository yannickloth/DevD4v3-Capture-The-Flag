
namespace CTF.Application.Tests.GameRules;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.GameRules</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
internal static class ChangeDrivers { }
