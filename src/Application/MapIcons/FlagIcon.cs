namespace CTF.Application.MapIcons;

/// <summary>
/// See <see href="https://www.open.mp/docs/scripting/resources/mapicons">map icons</see>.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapIcon)]
public enum FlagIcon
{
    /// <summary>The white flag icon.</summary>
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    White = 1,
    /// <summary>The red flag icon.</summary>
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    Red = 0,
    /// <summary>The blue flag icon.</summary>
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    Blue = Red
}
