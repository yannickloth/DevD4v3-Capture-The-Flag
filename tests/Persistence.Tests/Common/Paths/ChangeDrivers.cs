using CTF.Application.IVP;

namespace CTF.Application.Tests.Schema.MariaDbDialect.SqliteDialect;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Schema.MariaDbDialect.SqliteDialect</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect, ChangeDriver.SqliteDialect)]
internal static class ChangeDrivers { }
