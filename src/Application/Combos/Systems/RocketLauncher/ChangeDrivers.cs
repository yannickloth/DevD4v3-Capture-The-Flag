using CTF.Application.IVP;

namespace CTF.Application.Combos.ClientMessageDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Combos.ClientMessageDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.MapRotation, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
