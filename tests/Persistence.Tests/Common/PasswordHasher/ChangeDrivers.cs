using CTF.Application.IVP;

namespace Persistence.Tests.Common.PasswordHasher;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.Tests.Common.PasswordHasher</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.BCrypt)]
internal static class ChangeDrivers { }
