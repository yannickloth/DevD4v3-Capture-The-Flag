using CTF.Application.IVP;

namespace CTF.Application.Combat.RestoreVitalityCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.RestoreVitalityCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs, ChangeDriver.Player)]
internal static class ChangeDrivers { }
