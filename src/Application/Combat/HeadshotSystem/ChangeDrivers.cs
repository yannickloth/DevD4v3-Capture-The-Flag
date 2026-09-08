
namespace CTF.Application.Combat.RepositoryDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Combat.Connection.HeadshotSystem</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.Audio, ChangeDriver.Configuration, ChangeDriver.Statistics, ChangeDriver.Repository)]
internal static class ChangeDrivers { }
