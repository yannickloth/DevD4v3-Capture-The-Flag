namespace CTF.Application.GameRules;

/// <summary>
/// Provides team-membership extension methods over the player entity.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification); CD-01 (open.mp/SampSharp platform API) → CD-02</remarks>
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
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification)</remarks>
    public static Team RemoveFromCurrentTeam(this Player player)
    {
        if (player.Team == (int)TeamId.NoTeam)
            return Team.None;

        PlayerInfo playerInfo = player.GetRequiredInfo();
        Team currentTeam = playerInfo.Team;
        currentTeam.Members.Remove(player);
        playerInfo.SetTeam(TeamId.NoTeam);
        player.Team = (int)TeamId.NoTeam;
        player.Color = Team.None.ColorHex;
        return currentTeam;
    }
}
