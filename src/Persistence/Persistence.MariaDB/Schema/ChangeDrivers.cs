using CTF.Application.IVP;

namespace CTF.Application.SchemaNs4;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.SchemaNs4</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
