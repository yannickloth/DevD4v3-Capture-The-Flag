using CTF.Application.IVP;

namespace CTF.Host.Discord;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Host.Discord</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Discord, ChangeDriver.Logging)]
internal static class ChangeDrivers { }
