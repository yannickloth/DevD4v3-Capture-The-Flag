using CTF.Application.IVP;

namespace Persistence.InMemory.Models;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.InMemory.Models</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
internal static class ChangeDrivers { }
