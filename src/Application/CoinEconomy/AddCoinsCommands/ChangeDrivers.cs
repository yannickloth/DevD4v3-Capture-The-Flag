using CTF.Application.IVP;

namespace CTF.Application.CoinEconomy.AddCoinsCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.CoinEconomy.AddCoinsCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
