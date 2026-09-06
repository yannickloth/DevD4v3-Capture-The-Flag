using CTF.Application.IVP;

namespace CTF.Application.Commands.Moderator.ClearChatCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Moderator.ClearChatCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
