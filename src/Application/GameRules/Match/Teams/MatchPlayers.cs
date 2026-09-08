namespace CTF.Application.GameRules.PlayerNsNs2;

/// <summary>
/// Provides access to all players participating in the current match.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public static class MatchPlayers
{
    /// <summary>
    /// Gets all players participating in the current match.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static IEnumerable<Player> GetAll()
    {
        foreach (Player player in Team.Alpha.Members) 
            yield return player;

        foreach (Player player in Team.Beta.Members)
            yield return player;
    }
}
