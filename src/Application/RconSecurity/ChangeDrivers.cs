
namespace CTF.Application.RconSecurity.RconSecurityDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.RconSecurity</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.RconSecurity, ChangeDriver.Player, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
