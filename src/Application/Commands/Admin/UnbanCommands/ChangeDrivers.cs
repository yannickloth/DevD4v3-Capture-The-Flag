
namespace CTF.Application.Commands.ClientMessageNsNs2;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Admin.UnbanCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.ServerService, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
