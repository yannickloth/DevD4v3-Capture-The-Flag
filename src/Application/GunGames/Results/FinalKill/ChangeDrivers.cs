
namespace CTF.Application.GunGames.ClientMessageNsNs3;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.GunGames.FinalKill</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GunGame, ChangeDriver.Statistics, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
