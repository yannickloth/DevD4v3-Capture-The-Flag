using CTF.Application.IVP;

namespace CTF.Composition.SqliteDialectDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Composition.SqliteDialectDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Configuration, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect)]
internal static class ChangeDrivers { }
