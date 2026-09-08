using CTF.Application.IVP;

namespace CTF.Application.GameRules.AudioDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.GameRules.AudioDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
internal static class ChangeDrivers { }
