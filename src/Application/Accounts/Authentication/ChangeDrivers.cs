using CTF.Application.IVP;

namespace CTF.Application.Accounts.Authentication;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Accounts.Authentication</c> (root-first). Container for the
/// authentication clusters (dialogs, ECS, repository/player-event pairs), all CD-08.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account)]
internal static class ChangeDrivers { }
