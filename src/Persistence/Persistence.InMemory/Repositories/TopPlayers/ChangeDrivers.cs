using CTF.Application.IVP;

namespace Persistence.InMemory.Repositories.TopPlayers;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.InMemory.Repositories.TopPlayers</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
