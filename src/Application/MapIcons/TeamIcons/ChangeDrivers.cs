
namespace CTF.Application.MapIcons.MapIconDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.TeamIcons</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapIcon, ChangeDriver.Map, ChangeDriver.Pickup)]
internal static class ChangeDrivers { }
