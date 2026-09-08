
namespace CTF.Application.GameRules.CommandSetDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Teams</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Dialog, ChangeDriver.Player, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
