using CTF.Application.IVP;

namespace CTF.Application.Combat.VitalityCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.VitalityCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Player)]
internal static class ChangeDrivers { }
