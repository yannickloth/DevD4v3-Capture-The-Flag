
namespace CTF.Application.GameRules.GameTextDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.AtBase</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText)]
internal static class ChangeDrivers { }
