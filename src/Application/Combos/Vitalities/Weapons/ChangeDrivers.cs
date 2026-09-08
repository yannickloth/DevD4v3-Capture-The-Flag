
namespace CTF.Application.Combos.CombatDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Combos.RocketLauncher.Weapons</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.Coin, ChangeDriver.Combat)]
internal static class ChangeDrivers { }
