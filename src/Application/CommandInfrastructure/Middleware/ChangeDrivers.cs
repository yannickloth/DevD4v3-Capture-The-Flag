
namespace CTF.Application.CommandInfrastructure.MapRotationDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Middleware</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandInfrastructure, ChangeDriver.Ecs, ChangeDriver.Account, ChangeDriver.GameRules, ChangeDriver.MapRotation)]
internal static class ChangeDrivers { }
