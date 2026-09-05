using CTF.Application.IVP;

namespace CTF.Application.Tests.Players.Accounts.Team;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Players.Accounts.Team</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
internal static class ChangeDrivers { }
