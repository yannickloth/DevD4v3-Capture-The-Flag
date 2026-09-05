using CTF.Application.IVP;

namespace CTF.Application.Tests;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.Tests</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Hosting)]
internal static class ChangeDrivers { }
