using CTF.Application.IVP;

namespace Persistence.SQLite.Schema;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.SQLite.Schema</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
internal static class ChangeDrivers { }
