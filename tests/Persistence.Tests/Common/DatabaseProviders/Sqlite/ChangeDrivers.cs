using CTF.Application.IVP;

namespace Persistence.Tests.Common.DatabaseProviders.Sqlite;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.Tests.Common.DatabaseProviders.Sqlite</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
