using CTF.Application.IVP;

namespace CTF.Application.Tests.Players.Accounts;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Players.Accounts</c> (root-first). Container grouping the
/// PlayerInfo account-aggregate tests; leaf roots span Account/Statistics/Auth/GameRules,
/// so the eponymous CD-08 Account is used as the representative root.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Account)]
internal static class ChangeDrivers { }
