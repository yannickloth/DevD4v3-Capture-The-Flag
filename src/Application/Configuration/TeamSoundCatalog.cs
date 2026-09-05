namespace CTF.Application.Configuration;

/// <summary>
/// Holds the configured audio URLs for team flag events.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Configuration)]
public class TeamSoundCatalog
{
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string FlagDropped { get; private init; } = string.Empty;

    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string FlagReturned { get; private init; } = string.Empty;

    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string FlagTaken { get; private init; } = string.Empty;

    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public string TeamScores { get; private init; } = string.Empty;

    /// <summary>Gets the NoTeam catalog.</summary>
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public static readonly TeamSoundCatalog None = new();

    /// <summary>Gets the Alpha team catalog.</summary>
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
    public static readonly TeamSoundCatalog Alpha;

    /// <summary>Gets the Beta team catalog.</summary>
    [ChangeDriversAttribute(ChangeDriver.Configuration)]
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
