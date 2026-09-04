namespace CTF.Application.GameRules;

/// <summary>
/// Represents settings for class selection.
/// </summary>
/// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: ClassSelection__*); CD-40 (audio) → CD-17</remarks>
public class ClassSelectionSettings
{
    /// <summary>
    /// Gets the audio URL played during class selection.
    /// </summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: ClassSelection__IntroAudioUrl); CD-40 (audio) → CD-17</remarks>
    public string IntroAudioUrl { get; init; } = string.Empty;
}
