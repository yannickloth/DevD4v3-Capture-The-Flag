namespace CTF.Application.Teams.Statistics;

/// <summary>
/// Formats team statistics as textdraw-compatible strings.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team score); CD-02 (CTF game-rules specification: team identity) → CD-10; CD-01 (open.mp/SampSharp platform API: textdraw) → CD-10</remarks>
public static class TeamScoreFormatter
{
    /// <summary>
    /// Gets the team's score formatted for display in a textdraw.
    /// </summary>
    /// <param name="team">The team whose score should be formatted.</param>
    /// <returns>A textdraw-formatted team score string.</returns>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team score); CD-02 (CTF game-rules specification: team identity) → CD-10; CD-01 (open.mp/SampSharp platform API: textdraw) → CD-10</remarks>
    public static string GetScoreAsText(this Team team)
        => team == Team.None ? string.Empty : $"{team.Name}: {team.StatsPerRound.Score}";
}
