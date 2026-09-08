
namespace CTF.Application.Statistics;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Players</c> (root-first). Container grouping the player-account
/// aggregate test suites; its leaves span Account/Statistics/Auth/Combat/GameRules, so CD-10
/// Statistics (the most frequent leaf root) is used as the representative root.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
internal static class ChangeDrivers { }
