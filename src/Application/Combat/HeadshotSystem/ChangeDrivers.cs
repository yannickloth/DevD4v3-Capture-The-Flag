using CTF.Application.IVP;

namespace CTF.Application.Combat.HeadshotSystem;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.HeadshotSystem</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.Audio, ChangeDriver.Configuration, ChangeDriver.Statistics, ChangeDriver.Repository)]
internal static class ChangeDrivers { }
