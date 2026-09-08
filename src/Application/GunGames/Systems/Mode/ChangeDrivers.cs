
namespace CTF.Application.GunGames.CommandInfrastructureDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.GunGames.Mode</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
internal static class ChangeDrivers { }
