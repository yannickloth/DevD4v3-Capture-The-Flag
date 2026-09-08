
namespace CTF.Application.GameRules.RepositoryNsNs4;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Score</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Coin, ChangeDriver.Statistics, ChangeDriver.Repository)]
internal static class ChangeDrivers { }
