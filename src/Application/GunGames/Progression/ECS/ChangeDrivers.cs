using CTF.Application.IVP;

namespace CTF.Application.GunGames.Progression.ECS;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GunGames.Progression.ECS</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame)]
internal static class ChangeDrivers { }
