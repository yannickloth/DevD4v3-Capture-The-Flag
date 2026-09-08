
namespace CTF.Application.Tests.Repositories.DatabaseSchema;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Repositories.DatabaseSchema</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.DatabaseSchema)]
internal static class ChangeDrivers { }
