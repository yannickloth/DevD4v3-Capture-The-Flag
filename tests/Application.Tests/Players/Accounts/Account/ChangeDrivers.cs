
namespace CTF.Application.Tests.Accounts;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Accounts</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
internal static class ChangeDrivers { }
