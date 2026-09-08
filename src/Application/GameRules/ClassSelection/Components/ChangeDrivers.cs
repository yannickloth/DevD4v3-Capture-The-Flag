
namespace CTF.Application.GameRules.EcsNsNs2;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Components</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
