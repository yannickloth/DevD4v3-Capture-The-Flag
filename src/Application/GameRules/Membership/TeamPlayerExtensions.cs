namespace CTF.Application.GameRules.Membership;

/// <summary>
/// Provides team-membership extension methods over the player entity.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
public static class TeamPlayerExtensions
{
    /// <summary>
    /// Removes the specified player from their current team.
    /// </summary>
    /// <param name="player">
    /// The player to remove from the current team.
    /// </param>
    /// <returns>
    /// The team from which the player was removed, or <see cref="Team.None"/> if the player had no team.
    /// </returns>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public static Team RemoveFromCurrentTeam(this Player player)
    {
        if (player.Team == (int)TeamId.NoTeam)
            return Team.None;

        PlayerInfo playerInfo = player.GetRequiredInfo();
        Team currentTeam = playerInfo.Appearance.Team;
        currentTeam.Members.Remove(player);
        playerInfo.Appearance.SetTeam(TeamId.NoTeam);
        player.Team = (int)TeamId.NoTeam;
        player.Color = Team.None.ColorHex;
        return currentTeam;
    }
}
