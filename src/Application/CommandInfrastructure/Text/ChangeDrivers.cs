
namespace CTF.Application.CommandInfrastructure.PlayerDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Text</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandInfrastructure, ChangeDriver.Player)]
internal static class ChangeDrivers { }
