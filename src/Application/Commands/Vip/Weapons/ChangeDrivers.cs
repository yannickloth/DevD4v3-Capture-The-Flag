
namespace CTF.Application.Commands.PlayerNsNs3;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Weapons</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Player)]
internal static class ChangeDrivers { }
