
namespace CTF.Application.MapRotation.AuthorizationNsNs2;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.MapRotation.Commands.RotationTimer</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
internal static class ChangeDrivers { }
