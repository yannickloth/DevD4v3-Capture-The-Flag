
namespace CTF.Application.WeaponCatalogs.CommandInfrastructureDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.WeaponCatalogs.System</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.WeaponCatalog, ChangeDriver.Combat, ChangeDriver.GunGame, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
internal static class ChangeDrivers { }
