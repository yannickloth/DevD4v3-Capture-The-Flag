using CTF.Application.IVP;

namespace Persistence.Tests;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of the
/// <c>Persistence.Tests</c> assembly root (root-first). Test project for the repository
/// contracts (CD-20) exercised against each provider.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
