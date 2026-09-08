
namespace CTF.Application.GameRules.CompositionDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Events</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Composition)]
internal static class ChangeDrivers { }
