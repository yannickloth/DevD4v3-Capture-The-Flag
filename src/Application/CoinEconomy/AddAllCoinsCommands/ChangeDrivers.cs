using CTF.Application.IVP;

namespace CTF.Application.CoinEconomy.AddAllCoinsCommands;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.CoinEconomy.AddAllCoinsCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
