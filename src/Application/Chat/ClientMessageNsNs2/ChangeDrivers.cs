using CTF.Application.IVP;

namespace CTF.Application.Chat.ClientMessageNsNs2;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Chat.ClientMessageNsNs2</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Chat, ChangeDriver.Authorization, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
