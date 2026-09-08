
namespace CTF.Application.PlayerResources.RepositoryDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.PlayerResources.Commands</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Model, ChangeDriver.Player, ChangeDriver.CommandSet, ChangeDriver.Repository)]
internal static class ChangeDrivers { }
