using CTF.Application.IVP;

namespace CTF.Application.Teams.Ids;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Teams.Ids</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
internal static class ChangeDrivers { }
