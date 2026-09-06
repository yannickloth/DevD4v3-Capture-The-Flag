using CTF.Application.IVP;

namespace CTF.Application.GameRules.Flag.System.Death;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.Flag.System.Death</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Combat, ChangeDriver.Coin, ChangeDriver.Statistics)]
internal static class ChangeDrivers { }
