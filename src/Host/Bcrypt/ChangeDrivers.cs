using CTF.Application.IVP;

namespace CTF.Host.Bcrypt;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Host.Bcrypt</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.BCrypt)]
internal static class ChangeDrivers { }
