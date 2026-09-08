
namespace CTF.Application.Accounts.EcsNsNs2;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Credentials.RepositoryPlayerEvents</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
