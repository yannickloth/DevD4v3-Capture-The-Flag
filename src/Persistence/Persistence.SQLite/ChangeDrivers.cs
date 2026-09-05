using CTF.Application.IVP;

namespace Persistence.SQLite;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.SQLite</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
internal static class ChangeDrivers { }
