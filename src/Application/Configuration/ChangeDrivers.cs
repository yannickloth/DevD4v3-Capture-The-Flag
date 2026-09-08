using CTF.Application.IVP;

namespace CTF.Application.Configuration.AudioDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Configuration.AudioDomain</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Configuration, ChangeDriver.Audio)]
internal static class ChangeDrivers { }
