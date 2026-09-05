using CTF.Application.IVP;

namespace CTF.Host.Composition.DatabaseProviders;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Host.Composition.DatabaseProviders</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
internal static class ChangeDrivers { }
