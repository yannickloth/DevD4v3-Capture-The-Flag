
namespace CTF.Application.AntiCheat.EcsDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.ECS</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
