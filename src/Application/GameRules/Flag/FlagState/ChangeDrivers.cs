
namespace CTF.Application.GameRules.AttachedObjectDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.FlagState</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.MapIcon, ChangeDriver.Model, ChangeDriver.AttachedObject)]
internal static class ChangeDrivers { }
