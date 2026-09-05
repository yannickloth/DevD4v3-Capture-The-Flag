using CTF.Application.IVP;

namespace CTF.Application.Chat.TeamChat;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Chat.TeamChat</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat)]
internal static class ChangeDrivers { }
