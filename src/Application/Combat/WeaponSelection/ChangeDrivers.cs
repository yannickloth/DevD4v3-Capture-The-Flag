using CTF.Application.IVP;

namespace CTF.Application.Combat.WeaponSelection;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.WeaponSelection</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
