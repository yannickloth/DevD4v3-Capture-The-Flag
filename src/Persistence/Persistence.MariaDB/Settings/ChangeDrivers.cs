
namespace CTF.Application.Configuration;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.MariaDB.Settings</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Configuration)]
internal static class ChangeDrivers { }
