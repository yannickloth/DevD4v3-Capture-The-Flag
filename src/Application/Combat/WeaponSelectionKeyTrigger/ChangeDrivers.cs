
namespace CTF.Application.Combat.DialogDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.WeaponSelectionKeyTrigger</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Dialog)]
internal static class ChangeDrivers { }
