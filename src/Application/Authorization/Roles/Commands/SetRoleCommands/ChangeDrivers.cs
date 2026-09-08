
namespace CTF.Application.Authorization.CommandSetNsNs2;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Authorization.Roles.Commands.SetRoleCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
