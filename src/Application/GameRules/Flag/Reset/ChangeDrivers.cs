
namespace CTF.Application.GameRules.TimerNsNs2;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Reset</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Timer)]
internal static class ChangeDrivers { }
