
namespace CTF.Application.Chat.PlayerDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Chat.Commands.PrivateMessageConnect</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Player)]
internal static class ChangeDrivers { }
