
namespace CTF.Composition.EcsDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Composition.EcsBuilder</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
