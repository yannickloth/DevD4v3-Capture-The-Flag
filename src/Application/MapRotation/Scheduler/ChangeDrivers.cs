
namespace CTF.Application.MapRotation.ServerServiceDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.MapRotation.Scheduler</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.Timer, ChangeDriver.ServerService)]
internal static class ChangeDrivers { }
