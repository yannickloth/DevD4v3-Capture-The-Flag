using CTF.Application.IVP;

namespace CTF.Application.Commands.Basic.Spectate;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Basic.Spectate</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
