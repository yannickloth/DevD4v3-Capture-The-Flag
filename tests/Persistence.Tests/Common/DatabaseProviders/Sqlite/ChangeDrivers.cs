using CTF.Application.IVP;

namespace CTF.Application.Tests.Repositories.SqliteDialect.DatabaseSchema.Composition.BCrypt;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Repositories.SqliteDialect.DatabaseSchema.Composition.BCrypt</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.SqliteDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
internal static class ChangeDrivers { }
