namespace CTF.Application.GameRules;

/// <summary>
/// Represents the result of a match between two teams.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: match end conditions)</remarks>
public class MatchResult
{
    /// <summary>Gets the winning team, or <see cref="Team.None"/> for a tie.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: match end conditions)</remarks>
    public Team Winner { get; }

    /// <summary>Gets whether the match was a tie.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: match end conditions)</remarks>
    public bool IsTie => Winner == Team.None;

    private MatchResult(Team winner)
        => Winner = winner;

    /// <summary>Creates a match result from the two teams' scores.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: match end conditions)</remarks>
    public static MatchResult Create(Team firstTeam, Team secondTeam)
    {
        if (firstTeam.IsWinner())
            return new MatchResult(firstTeam);

        if (secondTeam.IsWinner())
            return new MatchResult(secondTeam);

        return new MatchResult(Team.None);
    }
}
