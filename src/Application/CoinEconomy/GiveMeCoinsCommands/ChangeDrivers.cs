
namespace CTF.Application.CoinEconomy.CommandSetNsNs4;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.CoinEconomy.GiveMeCoinsCommands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.Statistics, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
internal static class ChangeDrivers { }
