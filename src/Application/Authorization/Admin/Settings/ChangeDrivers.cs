
namespace CTF.Application.Authorization.ConfigurationDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Authorization.Admin.Settings</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
