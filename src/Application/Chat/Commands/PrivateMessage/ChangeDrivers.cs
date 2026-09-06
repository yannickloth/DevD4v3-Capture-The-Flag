using CTF.Application.IVP;

namespace CTF.Application.Chat.Commands.PrivateMessage;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Chat.Commands.PrivateMessage</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
