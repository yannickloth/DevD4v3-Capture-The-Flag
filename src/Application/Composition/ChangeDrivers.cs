using CTF.Application.IVP;

namespace CTF.Composition.ChatDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Composition.ChatDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition, ChangeDriver.Chat)]
internal static class ChangeDrivers { }
