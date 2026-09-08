
namespace CTF.Application.MapRotation.AuthorizationNsNs3;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.MapRotation.Commands.SetTimeLeft</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.CommandInfrastructure, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
internal static class ChangeDrivers { }
