using CTF.Application.IVP;

namespace Persistence.InMemory;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.InMemory</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
internal static class ChangeDrivers { }
