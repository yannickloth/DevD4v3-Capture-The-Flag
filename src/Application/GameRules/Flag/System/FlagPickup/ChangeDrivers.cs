using CTF.Application.IVP;

namespace CTF.Application.GameRules.Flag.System.FlagPickup;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.Flag.System.FlagPickup</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Player)]
internal static class ChangeDrivers { }
