namespace CTF.Application.Teams.Matches;

/// <summary>
/// Provides access to all players participating in the current match.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: match participants), CD-01 (open.mp/SampSharp platform API: player entity).</remarks>
public static class MatchPlayers
{
    /// <summary>
    /// Gets all players participating in the current match.
    /// </summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: match participants).</remarks>
    public static IEnumerable<Player> GetAll()
    {
        foreach (Player player in Team.Alpha.Members) 
            yield return player;

        foreach (Player player in Team.Beta.Members)
            yield return player;
    }
}
