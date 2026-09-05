using CTF.Application.IVP;

namespace CTF.Application.AntiCheat.ECS;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.AntiCheat.ECS</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.AntiCheat)]
internal static class ChangeDrivers { }
