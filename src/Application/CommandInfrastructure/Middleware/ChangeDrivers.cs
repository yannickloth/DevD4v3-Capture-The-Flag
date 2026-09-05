using CTF.Application.IVP;

namespace CTF.Application.CommandInfrastructure.Middleware;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>CTF.Application.CommandInfrastructure.Middleware</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandInfrastructure)]
internal static class ChangeDrivers { }
