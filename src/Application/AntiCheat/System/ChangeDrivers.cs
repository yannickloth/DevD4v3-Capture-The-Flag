
namespace CTF.Application.AntiCheat.GameTextDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Settings.System</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat, ChangeDriver.Configuration, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.GameText)]
internal static class ChangeDrivers { }
