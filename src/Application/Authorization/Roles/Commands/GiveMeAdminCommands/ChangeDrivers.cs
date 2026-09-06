using CTF.Application.IVP;

namespace CTF.Application.Authorization.Roles.Commands.GiveMeAdminCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Authorization.Roles.Commands.GiveMeAdminCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
