
namespace CTF.Application.Chat.EcsNsNs2;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Chat.Commands.PrivateMessage</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
