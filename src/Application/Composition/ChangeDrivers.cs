using CTF.Application.IVP;

namespace CTF.Application.Composition;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Composition</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
internal static class ChangeDrivers { }
