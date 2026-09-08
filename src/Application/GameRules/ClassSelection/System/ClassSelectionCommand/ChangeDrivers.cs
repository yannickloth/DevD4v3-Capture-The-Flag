
namespace CTF.Application.GameRules.CombatDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.ClassSelection.System.ClassSelectionCommand</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Combat)]
internal static class ChangeDrivers { }
