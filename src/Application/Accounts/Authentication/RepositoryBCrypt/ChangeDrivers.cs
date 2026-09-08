
namespace CTF.Application.Accounts.ClientMessageDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Credentials.RepositoryBCrypt</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.BCrypt, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
