
namespace CTF.Composition;

/// <summary>
/// Namespace marker declaring the causal change-driver chain of the
/// <c>CTF.Host</c> bootstrap root (root-first). Bootstrap namespace with no own top-level
/// types; CD-01 is retired, so it takes the root of its dominant child (CD-21 Composition).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Composition)]
internal static class ChangeDrivers { }
