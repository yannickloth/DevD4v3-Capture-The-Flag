
namespace Persistence.Repositories.ConfigurationNsNs3;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Player</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.MariaDbDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
