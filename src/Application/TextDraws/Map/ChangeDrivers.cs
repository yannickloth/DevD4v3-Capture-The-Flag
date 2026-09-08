
namespace CTF.Application.TextDraws.MapDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.TextDraws.Map</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.TextDraw, ChangeDriver.MapRotation, ChangeDriver.Map)]
internal static class ChangeDrivers { }
