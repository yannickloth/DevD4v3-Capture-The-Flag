using CTF.Application.IVP;

namespace CTF.Application.Teams;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Teams</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
internal static class ChangeDrivers { }
