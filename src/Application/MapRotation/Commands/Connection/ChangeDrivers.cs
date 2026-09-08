
namespace CTF.Application.MapRotation.PlayerDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.MapRotation.Commands.Connection</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Player)]
internal static class ChangeDrivers { }
