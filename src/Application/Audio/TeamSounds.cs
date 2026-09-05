namespace CTF.Application.Audio;

using CTF.Application.Configuration;

/// <summary>
/// Plays the configured team flag-event sounds.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Audio)]
public class TeamSounds
{
    private readonly TeamSoundCatalog _catalog;

    /// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
    public TeamSounds(TeamSoundCatalog catalog) => _catalog = catalog;

    /// <summary>Plays the sound when the team's flag is taken.</summary>
    [ChangeDriversAttribute(ChangeDriver.Audio)]
    public void PlayFlagTakenSound()
        => PlayAudioStreamToAll(_catalog.FlagTaken);

    /// <summary>Plays the sound when the team's flag is dropped.</summary>
    [ChangeDriversAttribute(ChangeDriver.Audio)]
    public void PlayFlagDroppedSound()
        => PlayAudioStreamToAll(_catalog.FlagDropped);

    /// <summary>Plays the sound when the team's flag is returned.</summary>
    [ChangeDriversAttribute(ChangeDriver.Audio)]
    public void PlayFlagReturnedSound()
        => PlayAudioStreamToAll(_catalog.FlagReturned);

    /// <summary>Plays the sound when the team scores.</summary>
    [ChangeDriversAttribute(ChangeDriver.Audio)]
    public void PlayTeamScoresSound()
        => PlayAudioStreamToAll(_catalog.TeamScores);

    /// <summary>Plays the audio stream to all match players.</summary>
    [ChangeDriversAttribute(ChangeDriver.Audio)]
    private static void PlayAudioStreamToAll(string url)
    {
        IEnumerable<Player> players = MatchPlayers.GetAll();
        foreach (Player player in players)
            player.PlayAudioStream(url);
    }
}
