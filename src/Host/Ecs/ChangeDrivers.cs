using CTF.Application.IVP;

namespace CTF.Host.Ecs;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Host.Ecs</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Ecs)]
internal static class ChangeDrivers { }
