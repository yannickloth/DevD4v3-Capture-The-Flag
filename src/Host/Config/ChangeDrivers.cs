
namespace CTF.Application.Configuration.CompositionDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Host.Config</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Configuration, ChangeDriver.Composition)]
internal static class ChangeDrivers { }
