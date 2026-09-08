
namespace CTF.Application.CoinEconomy.EcsDomain;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.CoinEconomy.WaitTimes</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Configuration, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
