using CTF.Application.IVP;

namespace CTF.Application.SchemaNs3;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.SchemaNs3</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema, ChangeDriver.BCrypt)]
internal static class ChangeDrivers { }
