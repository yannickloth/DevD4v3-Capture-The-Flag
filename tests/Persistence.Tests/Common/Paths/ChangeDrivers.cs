using CTF.Application.IVP;

namespace Persistence.Tests.Common.Paths;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.Tests.Common.Paths</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
internal static class ChangeDrivers { }
