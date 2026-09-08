
namespace CTF.Application.Statistics.ConfigurationDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Core.Limits</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
