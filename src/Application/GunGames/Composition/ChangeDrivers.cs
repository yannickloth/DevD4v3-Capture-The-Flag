using CTF.Application.IVP;

namespace CTF.Application.GunGames.Composition;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GunGames.Composition</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
internal static class ChangeDrivers { }
