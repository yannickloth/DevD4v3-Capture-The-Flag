using CTF.Application.IVP;

namespace CTF.Application.Tests.Players.Accounts.Core;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Players.Accounts.Core</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Model)]
internal static class ChangeDrivers { }
