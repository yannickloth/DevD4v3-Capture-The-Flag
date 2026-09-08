using CTF.Application.IVP;

namespace CTF.Application.Accounts.RepositoryNsNs2;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Accounts.RepositoryNsNs2</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.GunGame, ChangeDriver.BCrypt, ChangeDriver.Repository)]
internal static class ChangeDrivers { }
