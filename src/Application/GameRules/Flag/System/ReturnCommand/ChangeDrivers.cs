using CTF.Application.IVP;

namespace CTF.Application.GameRules.Flag.System.ReturnCommand;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.Flag.System.ReturnCommand</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer)]
internal static class ChangeDrivers { }
