
namespace CTF.Application.Authorization.EcsDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Authorization.Roles.FailedAttempts</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
