
namespace CTF.Application.Accounts.CoinDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Credentials.Profile</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account, ChangeDriver.Coin)]
internal static class ChangeDrivers { }
