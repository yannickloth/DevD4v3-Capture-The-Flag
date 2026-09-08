
namespace Persistence.Repositories.ConfigurationNsNs5;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Player</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.SqliteDialect, ChangeDriver.BCrypt, ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
