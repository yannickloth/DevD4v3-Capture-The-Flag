namespace CTF.Application.Statistics;

/// <summary>
/// Represents the per-round statistics for a team.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team stats)</remarks>
public class TeamStatsPerRound
{
    /// <summary>Gets the team's score.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team score); CD-02 (CTF game-rules specification: scoring) → CD-10</remarks>
    public int Score { get; private set; }

    /// <summary>Gets the team's kills.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team kills)</remarks>
    public int Kills { get; private set; }

    /// <summary>Gets the team's deaths.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team deaths)</remarks>
    public int Deaths { get; private set; }

    /// <summary>Adds a score to the team.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team score); CD-02 (CTF game-rules specification: scoring) → CD-10</remarks>
    public void AddScore()  => Score++;

    /// <summary>Adds a kill to the team.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team kills)</remarks>
    public void AddKills()  => Kills++;

    /// <summary>Adds a death to the team.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team deaths)</remarks>
    public void AddDeaths() => Deaths++;

    /// <summary>Resets the team's per-round statistics.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: round reset)</remarks>
    public void Reset()
    {
        Score = 0; 
        Kills = 0; 
        Deaths = 0;
    }
}
