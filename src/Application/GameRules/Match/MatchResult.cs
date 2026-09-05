namespace CTF.Application.GameRules.Match;

/// <summary>
/// Represents the result of a match between two teams.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules)]
public class MatchResult
{
    /// <summary>Gets the winning team, or <see cref="Team.None"/> for a tie.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public Team Winner { get; }

    /// <summary>Gets whether the match was a tie.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public bool IsTie => Winner == Team.None;

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: match end conditions)</remarks>
    private MatchResult(Team winner)
        => Winner = winner;

    /// <summary>Creates a match result from the two teams' scores.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static MatchResult Create(Team firstTeam, Team secondTeam)
    {
        if (firstTeam.IsWinner())
            return new MatchResult(firstTeam);

        if (secondTeam.IsWinner())
            return new MatchResult(secondTeam);

        return new MatchResult(Team.None);
    }
}
