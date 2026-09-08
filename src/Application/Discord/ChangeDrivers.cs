
namespace CTF.Application.Discord.EcsDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Discord</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Discord, ChangeDriver.Player, ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
