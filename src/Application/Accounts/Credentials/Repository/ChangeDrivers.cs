using CTF.Application.IVP;

namespace CTF.Application.Accounts.Credentials.Repository;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Accounts.Credentials.Repository</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.DatabaseSchema, ChangeDriver.Repository)]
internal static class ChangeDrivers { }
