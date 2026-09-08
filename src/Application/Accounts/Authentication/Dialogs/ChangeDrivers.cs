
namespace CTF.Application.Accounts.DialogDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Credentials.Dialogs</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Dialog)]
internal static class ChangeDrivers { }
