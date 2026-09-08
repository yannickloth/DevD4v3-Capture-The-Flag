
namespace CTF.Application.Authorization.CommandInfrastructureNsNs3;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Roles.Commands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
internal static class ChangeDrivers { }
