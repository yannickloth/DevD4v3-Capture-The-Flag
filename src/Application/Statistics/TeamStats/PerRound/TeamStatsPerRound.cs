namespace CTF.Application.Statistics.TeamStats.PerRound;

/// <summary>
/// Represents the per-round statistics for a team.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics)]
public class TeamStatsPerRound
{
    /// <summary>Gets the team's score.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int Score { get; private set; }

    /// <summary>Gets the team's kills.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int Kills { get; private set; }

    /// <summary>Gets the team's deaths.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public int Deaths { get; private set; }

    /// <summary>Adds a score to the team.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddScore()  => Score++;

    /// <summary>Adds a kill to the team.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddKills()  => Kills++;

    /// <summary>Adds a death to the team.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void AddDeaths() => Deaths++;

    /// <summary>Resets the team's per-round statistics.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public void Reset()
    {
        Score = 0; 
        Kills = 0; 
        Deaths = 0;
    }
}
