namespace CTF.Application.Configuration;

/// <summary>
/// Represents settings for class selection.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Configuration, ChangeDriver.Audio)]
public class ClassSelectionSettings
{
    /// <summary>
    /// Gets the audio URL played during class selection.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Configuration, ChangeDriver.Audio)]
    public string IntroAudioUrl { get; init; } = string.Empty;
}
