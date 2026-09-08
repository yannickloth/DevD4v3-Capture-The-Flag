using CTF.Application.IVP;

namespace CTF.Application.Tests.Players;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Players</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Player, ChangeDriver.NSubstitute)]
internal static class ChangeDrivers { }
