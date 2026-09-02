namespace CTF.Application.Teams.ClassSelection;

/// <summary>
/// Represents settings for class selection.
/// </summary>
/// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: ClassSelection__*); CD-01 (open.mp/SampSharp platform API: audio) → CD-17</remarks>
public class ClassSelectionSettings
{
    /// <summary>
    /// Gets the audio URL played during class selection.
    /// </summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: ClassSelection__IntroAudioUrl); CD-01 (open.mp/SampSharp platform API: audio) → CD-17</remarks>
    public string IntroAudioUrl { get; init; } = string.Empty;
}
