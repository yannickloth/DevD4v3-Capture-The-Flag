using CTF.Application.IVP;

namespace CTF.Application.AntiCheat.ConfigurationDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.AntiCheat.ConfigurationDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
