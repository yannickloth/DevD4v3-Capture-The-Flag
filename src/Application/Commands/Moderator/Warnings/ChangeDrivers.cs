
namespace CTF.Application.Commands.EcsDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Commands.Moderator.Warnings</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
