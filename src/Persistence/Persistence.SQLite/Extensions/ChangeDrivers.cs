
namespace Persistence.SQLite.Extensions;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.SQLite.Extensions</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.SqliteDialect)]
internal static class ChangeDrivers { }
