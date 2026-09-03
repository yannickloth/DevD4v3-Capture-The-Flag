namespace CTF.Application.Platform;

/// <summary>
/// Represents the sounds played for team flag events.
/// </summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: audio); CD-17 (game configuration/.env schema: audio URLs) → CD-01</remarks>
public class TeamSounds
{
    private string _flagDropped;
    private string _flagReturned;
    private string _flagTaken;
    private string _teamScores;

    /// <summary>Gets the NoTeam sounds.</summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public static readonly TeamSounds None;
    /// <summary>Gets the Alpha team sounds.</summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public static readonly TeamSounds Alpha;
    /// <summary>Gets the Beta team sounds.</summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public static readonly TeamSounds Beta;

    static TeamSounds()
    {
        var reader = new EnvReader();
        var defaultValue = string.Empty;

        Alpha = new()
        {
            _flagDropped  = reader.EnvString("RedFlagDroppedUrl",  defaultValue),
            _flagReturned = reader.EnvString("RedFlagReturnedUrl", defaultValue),
            _flagTaken    = reader.EnvString("RedFlagTakenUrl",    defaultValue),
            _teamScores   = reader.EnvString("RedTeamScoresUrl",   defaultValue)
        };

        Beta = new()
        {
            _flagDropped  = reader.EnvString("BlueFlagDroppedUrl",  defaultValue),
            _flagReturned = reader.EnvString("BlueFlagReturnedUrl", defaultValue),
            _flagTaken    = reader.EnvString("BlueFlagTakenUrl",    defaultValue),
            _teamScores   = reader.EnvString("BlueTeamScoresUrl",   defaultValue)
        };

        None = new();
    }

    private TeamSounds() { }

    /// <summary>
    /// Plays the sound when the team's flag is taken.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: audio)</remarks>
    public void PlayFlagTakenSound()
        => PlayAudioStreamToAll(_flagTaken);

    /// <summary>
    /// Plays the sound when the team's flag is dropped.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: audio)</remarks>
    public void PlayFlagDroppedSound()
        => PlayAudioStreamToAll(_flagDropped);

    /// <summary>
    /// Plays the sound when the team's flag is returned.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: audio)</remarks>
    public void PlayFlagReturnedSound()
        => PlayAudioStreamToAll(_flagReturned);

    /// <summary>
    /// Plays the sound when the team scores.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: audio)</remarks>
    public void PlayTeamScoresSound()
        => PlayAudioStreamToAll(_teamScores);

    /// <summary>Plays the audio stream to all match players.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: audio)</remarks>
    private static void PlayAudioStreamToAll(string url)
    {
        IEnumerable<Player> players = MatchPlayers.GetAll();
        foreach (Player player in players)
            player.PlayAudioStream(url);
    }
}
