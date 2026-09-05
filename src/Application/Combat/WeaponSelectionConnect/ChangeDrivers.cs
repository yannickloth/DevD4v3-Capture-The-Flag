using CTF.Application.IVP;

namespace CTF.Application.Combat.WeaponSelectionConnect;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.WeaponSelectionConnect</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
internal static class ChangeDrivers { }
