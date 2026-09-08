
namespace CTF.Application.Tests.Repositories.MariaDbDialect.SqliteDialect.Composition;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Repositories.Composition</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect, ChangeDriver.Composition)]
internal static class ChangeDrivers { }
