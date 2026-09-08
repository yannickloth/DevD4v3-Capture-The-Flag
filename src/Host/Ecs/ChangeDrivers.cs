
namespace CTF.Host.Ecs.EcsDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Ecs</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Ecs, ChangeDriver.Configuration, ChangeDriver.Composition, ChangeDriver.Logging, ChangeDriver.Discord)]
internal static class ChangeDrivers { }
