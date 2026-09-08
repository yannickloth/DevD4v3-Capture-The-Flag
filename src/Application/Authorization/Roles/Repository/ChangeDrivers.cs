
namespace CTF.Application.Authorization.RepositoryDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Authorization.Roles.Repository</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository)]
internal static class ChangeDrivers { }
