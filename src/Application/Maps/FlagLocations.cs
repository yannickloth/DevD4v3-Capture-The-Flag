namespace CTF.Application.Maps;

/// <remarks>Change drivers: CD-11 (map configuration)</remarks>
public class FlagLocations
{
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public static readonly FlagLocations Empty = new()
    {
        Red  = new Vector3(0f, 0f, 0f),
        Blue = new Vector3(0f, 0f, 0f)
    };

    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public required Vector3 Red { get; init; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public required Vector3 Blue { get; init; }
}
