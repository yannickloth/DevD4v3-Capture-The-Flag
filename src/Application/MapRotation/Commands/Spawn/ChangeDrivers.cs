
namespace CTF.Application.MapRotation.TextDrawDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.MapRotation.Commands.Spawn</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Player, ChangeDriver.TextDraw)]
internal static class ChangeDrivers { }
