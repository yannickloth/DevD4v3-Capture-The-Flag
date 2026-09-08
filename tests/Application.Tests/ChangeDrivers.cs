using CTF.Application.IVP;

namespace CTF.Application.Tests.Hosting.Map;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Tests.Hosting.Map</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Hosting, ChangeDriver.NUnit, ChangeDriver.FluentAssertions, ChangeDriver.Map)]
internal static class ChangeDrivers { }
