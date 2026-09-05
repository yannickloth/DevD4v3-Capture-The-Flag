namespace CTF.Application.Configuration;

/// <summary>
/// Holds the configured audio URLs for team flag events.
/// </summary>
/// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
public class TeamSoundCatalog
{
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public string FlagDropped { get; private init; } = string.Empty;

    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public string FlagReturned { get; private init; } = string.Empty;

    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public string FlagTaken { get; private init; } = string.Empty;

    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public string TeamScores { get; private init; } = string.Empty;

    /// <summary>Gets the NoTeam catalog.</summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public static readonly TeamSoundCatalog None = new();

    /// <summary>Gets the Alpha team catalog.</summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public static readonly TeamSoundCatalog Alpha;

    /// <summary>Gets the Beta team catalog.</summary>
    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    public static readonly TeamSoundCatalog Beta;

    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    static TeamSoundCatalog()
    {
        var reader = new EnvReader();
        var defaultValue = string.Empty;

        Alpha = new()
        {
            FlagDropped  = reader.EnvString("RedFlagDroppedUrl",  defaultValue),
            FlagReturned = reader.EnvString("RedFlagReturnedUrl", defaultValue),
            FlagTaken    = reader.EnvString("RedFlagTakenUrl",    defaultValue),
            TeamScores   = reader.EnvString("RedTeamScoresUrl",   defaultValue)
        };

        Beta = new()
        {
            FlagDropped  = reader.EnvString("BlueFlagDroppedUrl",  defaultValue),
            FlagReturned = reader.EnvString("BlueFlagReturnedUrl", defaultValue),
            FlagTaken    = reader.EnvString("BlueFlagTakenUrl",    defaultValue),
            TeamScores   = reader.EnvString("BlueTeamScoresUrl",   defaultValue)
        };
    }

    /// <remarks>Change drivers: CD-17 (root; game configuration/.env schema: audio URLs)</remarks>
    private TeamSoundCatalog() { }
}
