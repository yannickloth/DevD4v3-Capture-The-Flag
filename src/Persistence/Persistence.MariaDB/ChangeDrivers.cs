using CTF.Application.IVP;

namespace Persistence.MariaDB;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.MariaDB</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
internal static class ChangeDrivers { }
