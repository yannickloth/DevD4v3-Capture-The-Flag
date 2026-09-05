using CTF.Application.IVP;

namespace Persistence.MariaDB.Repositories.TopPlayers;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.MariaDB.Repositories.TopPlayers</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
