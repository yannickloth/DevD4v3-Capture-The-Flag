
namespace CTF.Application.Pickups.PickupDomain;

/// <summary>
/// Namespace declaring the causal change-driver chain of
/// <c>CTF.Application.Pickups</c> (root-first).
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Pickup, ChangeDriver.Model, ChangeDriver.Map, ChangeDriver.ClientMessage)]
internal static class ChangeDrivers { }
