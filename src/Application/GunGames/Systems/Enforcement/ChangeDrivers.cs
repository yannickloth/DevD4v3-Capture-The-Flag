
namespace CTF.Application.GunGames.EcsNsNs3;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GunGames.Systems.Enforcement</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
