using CTF.Application.IVP;

namespace CTF.Application.Combat.VitalityToAllCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.VitalityToAllCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.Player)]
internal static class ChangeDrivers { }
