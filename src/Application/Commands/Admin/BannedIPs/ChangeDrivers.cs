
namespace CTF.Application.Commands.ClientMessageNsNs3;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Admin.BannedIPs</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
