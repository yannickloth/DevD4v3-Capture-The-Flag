using CTF.Application.IVP;

namespace CTF.Application.AntiCheat.SystemConnect;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.AntiCheat.SystemConnect</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Player)]
internal static class ChangeDrivers { }
