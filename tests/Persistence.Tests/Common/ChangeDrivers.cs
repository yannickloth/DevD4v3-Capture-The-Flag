using CTF.Application.IVP;

namespace Persistence.Tests.Common;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of
/// <c>Persistence.Tests.Common</c> (root-first). Shared repository/provider test harness.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Repository)]
internal static class ChangeDrivers { }
