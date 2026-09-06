using CTF.Application.IVP;

namespace CTF.Application.Commands.Basic.ReportPlayer;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Basic.ReportPlayer</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
