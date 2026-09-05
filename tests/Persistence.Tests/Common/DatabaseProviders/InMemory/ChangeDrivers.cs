using CTF.Application.IVP;

namespace Persistence.Tests.Common.DatabaseProviders.InMemory;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.Tests.Common.DatabaseProviders.InMemory</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
