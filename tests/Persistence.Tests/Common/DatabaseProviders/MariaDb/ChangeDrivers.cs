using CTF.Application.IVP;

namespace CTF.Application.Tests.Repositories.MariaDbDialect.DatabaseSchema.Composition.BCrypt;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Repositories.MariaDbDialect.DatabaseSchema.Composition.BCrypt</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.MariaDbDialect, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
internal static class ChangeDrivers { }
