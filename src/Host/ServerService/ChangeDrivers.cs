
namespace CTF.Host.ServerService.ServerServiceDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.ServerService</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.ServerService, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
