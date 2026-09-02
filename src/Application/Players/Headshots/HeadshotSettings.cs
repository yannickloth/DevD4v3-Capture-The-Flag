namespace CTF.Application.Players.Headshots;

/// <summary>
/// Represents settings for headshot events.
/// </summary>
/// <remarks>Change drivers: CD-03 (combat/weapon-rules specification), CD-17 (game configuration/.env schema)</remarks>
public class HeadshotSettings
{
    /// <summary>
    /// Gets the audio URL played when a player performs a headshot.
    /// </summary>
    /// <remarks>Change drivers: CD-03 (combat/weapon-rules specification), CD-17 (game configuration/.env schema)</remarks>
    public string AudioUrl { get; init; } = string.Empty;
}
