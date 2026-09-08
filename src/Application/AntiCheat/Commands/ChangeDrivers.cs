
namespace CTF.Application.AntiCheat.PlayerNsNs2;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Commands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Player)]
internal static class ChangeDrivers { }
