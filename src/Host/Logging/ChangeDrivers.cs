
namespace CTF.Host.Logging.LoggingDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Logging</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Logging, ChangeDriver.Composition, ChangeDriver.Discord)]
internal static class ChangeDrivers { }
