using CTF.Application.IVP;

namespace CTF.Composition.MariaDbDialectDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Composition.MariaDbDialectDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Configuration, ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect)]
internal static class ChangeDrivers { }
