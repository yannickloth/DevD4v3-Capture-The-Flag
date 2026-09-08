
namespace CTF.Application.Combat.ConfigurationDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.Headshot</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
