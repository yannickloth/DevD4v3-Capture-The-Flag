using CTF.Application.IVP;

namespace CTF.Application.GameRules.Membership;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.Membership</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
internal static class ChangeDrivers { }
