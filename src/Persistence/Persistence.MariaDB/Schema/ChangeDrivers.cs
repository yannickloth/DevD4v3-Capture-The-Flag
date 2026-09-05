using CTF.Application.IVP;

namespace Persistence.MariaDB.Schema;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.MariaDB.Schema</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
internal static class ChangeDrivers { }
