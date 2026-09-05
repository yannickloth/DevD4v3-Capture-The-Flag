using CTF.Application.IVP;

namespace Persistence.SQLite.Repositories;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.SQLite.Repositories</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
