
namespace CTF.Application.Maps.ServerServiceDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Maps.Initialization</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.Configuration, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.ServerService)]
internal static class ChangeDrivers { }
