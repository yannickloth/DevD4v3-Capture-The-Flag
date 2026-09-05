using CTF.Application.IVP;

namespace Persistence.Tests.Common.Factory;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.Tests.Common.Factory</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
