
namespace CTF.Application.Combat.PlayerNsNs3;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.WeaponSelectionSpawn</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.GunGame, ChangeDriver.Player)]
internal static class ChangeDrivers { }
