
namespace CTF.Application.Tests.Accounts.Ecs;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Accounts.Ecs</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
