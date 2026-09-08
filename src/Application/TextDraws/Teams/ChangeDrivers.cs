
namespace CTF.Application.TextDraws.ClientMessageDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.TextDraws.Teams</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.TextDraw, ChangeDriver.GameRules, ChangeDriver.Statistics, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
