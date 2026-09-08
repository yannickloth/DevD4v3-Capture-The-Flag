
namespace CTF.Application.Combos.StatisticsDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Combos.Purchase</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.CommandSet, ChangeDriver.Coin, ChangeDriver.GunGame, ChangeDriver.Player, ChangeDriver.Dialog, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.Statistics)]
internal static class ChangeDrivers { }
