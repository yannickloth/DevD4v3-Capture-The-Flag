
namespace CTF.Application.GameRules.EcsNsNs3;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Membership.Components</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
