using CTF.Application.IVP;

namespace Persistence.InMemory.Repositories;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.InMemory.Repositories</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
