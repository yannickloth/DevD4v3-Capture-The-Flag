using CTF.Application.IVP;

namespace CTF.Application.GunGames.Rewards;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.GunGames.Rewards</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
