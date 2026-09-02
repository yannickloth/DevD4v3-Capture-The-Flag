namespace CTF.Application.Teams.Flags;

/// <summary>
/// See <see href="https://www.open.mp/docs/scripting/resources/mapicons">map icons</see>.
/// </summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map-icon ids)</remarks>
public enum FlagIcon
{
    /// <summary>The white flag icon.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map-icon id)</remarks>
    White = 1,
    /// <summary>The red flag icon.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map-icon id)</remarks>
    Red = 0,
    /// <summary>The blue flag icon.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map-icon id)</remarks>
    Blue = Red
}
