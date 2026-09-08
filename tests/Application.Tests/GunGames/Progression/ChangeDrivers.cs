using CTF.Application.IVP;

namespace CTF.Application.Tests.GunGames;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.GunGames</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.NUnit, ChangeDriver.FluentAssertions)]
internal static class ChangeDrivers { }
