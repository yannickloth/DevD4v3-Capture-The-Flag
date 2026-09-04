namespace CTF.Application.Audio;

/// <summary>
/// Plays the configured team flag-event sounds.
/// </summary>
/// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
public class TeamSounds
{
    private readonly TeamSoundCatalog _catalog;

    /// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
    public TeamSounds(TeamSoundCatalog catalog) => _catalog = catalog;

    /// <summary>Plays the sound when the team's flag is taken.</summary>
    /// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
    public void PlayFlagTakenSound()
        => PlayAudioStreamToAll(_catalog.FlagTaken);

    /// <summary>Plays the sound when the team's flag is dropped.</summary>
    /// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
    public void PlayFlagDroppedSound()
        => PlayAudioStreamToAll(_catalog.FlagDropped);

    /// <summary>Plays the sound when the team's flag is returned.</summary>
    /// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
    public void PlayFlagReturnedSound()
        => PlayAudioStreamToAll(_catalog.FlagReturned);

    /// <summary>Plays the sound when the team scores.</summary>
    /// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
    public void PlayTeamScoresSound()
        => PlayAudioStreamToAll(_catalog.TeamScores);

    /// <summary>Plays the audio stream to all match players.</summary>
    /// <remarks>Change drivers: CD-40 (root; audio API)</remarks>
    private static void PlayAudioStreamToAll(string url)
    {
        IEnumerable<Player> players = MatchPlayers.GetAll();
        foreach (Player player in players)
            player.PlayAudioStream(url);
    }
}
