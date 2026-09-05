using CTF.Application.IVP;

namespace CTF.Host.Deployment;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Host.Deployment</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Hosting)]
internal static class ChangeDrivers { }
