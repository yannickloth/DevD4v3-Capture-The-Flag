using CTF.Application.IVP;

namespace CTF.Application.Combat.Connection;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.WeaponSelectionParachuteTrigger</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
internal static class ChangeDrivers { }
