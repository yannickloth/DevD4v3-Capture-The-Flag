using CTF.Application.IVP;

namespace CTF.Application.Commands.Admin.ShowAdminCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Admin.ShowAdminCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
internal static class ChangeDrivers { }
