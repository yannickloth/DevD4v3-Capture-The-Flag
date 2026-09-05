using CTF.Application.IVP;

namespace CTF.Application.Accounts;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Accounts</c> (root-first). CD-08 is the causal head;
/// the persistence/authentication sub-drivers that realize it are subordinated
/// drivers, not additional roots on this namespace.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account)]
internal static class ChangeDrivers { }
