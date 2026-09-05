using CTF.Application.IVP;

namespace Persistence.InMemory.Ids;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.InMemory.Ids</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
internal static class ChangeDrivers { }
