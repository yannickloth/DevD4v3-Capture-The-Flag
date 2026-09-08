
namespace CTF.Application.GameRules.AccountDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.ClassSelection.System.ClassSelectionDisconnect</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Account)]
internal static class ChangeDrivers { }
