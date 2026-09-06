using CTF.Application.IVP;

namespace CTF.Application.CoinEconomy.CoinCooldownConnect;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.CoinEconomy.CoinCooldownConnect</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Player)]
internal static class ChangeDrivers { }
