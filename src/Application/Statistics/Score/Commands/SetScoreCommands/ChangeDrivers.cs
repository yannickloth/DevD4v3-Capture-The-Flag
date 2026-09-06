using CTF.Application.IVP;

namespace CTF.Application.Statistics.Score.Commands.SetScoreCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Statistics.Score.Commands.SetScoreCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
