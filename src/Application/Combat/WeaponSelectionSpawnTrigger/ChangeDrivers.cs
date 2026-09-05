using CTF.Application.IVP;

namespace CTF.Application.Combat.WeaponSelectionSpawnTrigger;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.WeaponSelectionSpawnTrigger</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.GunGame, ChangeDriver.Player, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
