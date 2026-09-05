using CTF.Application.IVP;

namespace CTF.Application.GunGames.Systems;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GunGames.Systems</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
internal static class ChangeDrivers { }
