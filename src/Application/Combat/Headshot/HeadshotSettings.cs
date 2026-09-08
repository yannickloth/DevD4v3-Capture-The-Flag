namespace CTF.Application.Combat.ConfigurationDomain;

/// <summary>
/// Represents settings for headshot events.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Configuration)]
public class HeadshotSettings
{
    /// <summary>
    /// Gets the audio URL played when a player performs a headshot.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Configuration)]
    public string AudioUrl { get; init; } = string.Empty;
}
