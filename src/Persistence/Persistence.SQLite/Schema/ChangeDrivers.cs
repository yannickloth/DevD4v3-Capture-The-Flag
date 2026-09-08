using CTF.Application.IVP;

namespace CTF.Application.SchemaNs5;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.SchemaNs5</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
