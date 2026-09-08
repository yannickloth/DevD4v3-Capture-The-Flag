
namespace CTF.Application.Tests.Repositories.DatabaseSchema.Composition.BCrypt;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Repositories.BCrypt</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository, ChangeDriver.DatabaseSchema, ChangeDriver.Composition, ChangeDriver.BCrypt)]
internal static class ChangeDrivers { }
