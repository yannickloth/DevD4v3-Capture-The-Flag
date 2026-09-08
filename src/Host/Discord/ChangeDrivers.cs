
namespace CTF.Application.Discord.LoggingDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Host.Discord</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Discord, ChangeDriver.Logging)]
internal static class ChangeDrivers { }
