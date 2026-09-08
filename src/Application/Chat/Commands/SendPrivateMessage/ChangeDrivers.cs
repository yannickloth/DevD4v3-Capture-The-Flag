
namespace CTF.Application.Chat.EcsNsNs3;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Chat.Commands.SendPrivateMessage</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
