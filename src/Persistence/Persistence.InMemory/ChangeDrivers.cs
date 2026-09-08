using CTF.Application.IVP;

namespace CTF.Composition.DatabaseSchemaDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Composition.DatabaseSchemaDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.DatabaseSchema)]
internal static class ChangeDrivers { }
