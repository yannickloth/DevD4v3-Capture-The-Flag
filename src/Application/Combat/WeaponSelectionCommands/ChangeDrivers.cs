
namespace CTF.Application.Combat.CommandInfrastructureNsNs3;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.WeaponSelectionCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.WeaponCatalog, ChangeDriver.GunGame, ChangeDriver.CommandSet, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
internal static class ChangeDrivers { }
