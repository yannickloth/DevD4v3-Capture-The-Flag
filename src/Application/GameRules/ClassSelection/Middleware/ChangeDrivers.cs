
namespace CTF.Application.GameRules.MapRotationDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Components.Middleware</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Ecs, ChangeDriver.Account, ChangeDriver.MapRotation)]
internal static class ChangeDrivers { }
