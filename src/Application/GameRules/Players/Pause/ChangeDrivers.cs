using CTF.Application.IVP;

namespace CTF.Application.GameRules.Players.Pause;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.Players.Pause</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Timer)]
internal static class ChangeDrivers { }
