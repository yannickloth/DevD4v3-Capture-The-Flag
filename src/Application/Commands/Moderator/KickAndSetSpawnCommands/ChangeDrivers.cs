using CTF.Application.IVP;

namespace CTF.Application.Commands.Moderator.KickAndSetSpawnCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Moderator.KickAndSetSpawnCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
