
namespace CTF.Application.Chat.EcsNsNs4;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Chat.Commands.PrivateMessageToggle</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
