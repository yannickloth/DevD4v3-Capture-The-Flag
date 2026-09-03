namespace CTF.Application.GameRules;

/// <summary>
/// Balances players between two teams based on their score.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balancing); CD-01 (open.mp/SampSharp platform API: player team/score) → CD-02</remarks>
/// <remarks>Injected dependencies: teamTextDrawRenderer -> CD-01. Driven by the TeamTextDrawRenderer (rendering) contract + CD-21 (DI wiring).</remarks>
public class TeamBalancer(TeamTextDrawRenderer teamTextDrawRenderer)
{
    /// <summary>
    /// Reassigns players between the specified teams and invokes
    /// the callback after each player has been assigned.
    /// </summary>
    /// <param name="firstTeam">
    /// The first team participating in the balance operation.
    /// </param>
    /// <param name="secondTeam">
    /// The second team participating in the balance operation.
    /// </param>
    /// <param name="onPlayerAssigned">
    /// Invoked after a player has been assigned to a team.
    /// </param>
    /// <remarks>
    /// Players are sorted by score in descending order and then
    /// alternately assigned to the specified teams.
    /// </remarks>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balancing)</remarks>
    public void Balance(
        Team firstTeam,
        Team secondTeam, 
        Action<Player, PlayerInfo> onPlayerAssigned)
    {
        Player[] players = MatchPlayers.GetAll()
            .OrderByDescending(player => player.Score)
            .ToArray();

        firstTeam.Reset();
        secondTeam.Reset();

        for (int index = 0; index < players.Length; index++)
        {
            Player player = players[index];
            PlayerInfo playerInfo = player.GetRequiredInfo();
            if (player.IsPaused())
            {
                playerInfo.StatsPerRound.ResetStats();
                playerInfo.SetTeam(TeamId.NoTeam);
                player.Team = (int)TeamId.NoTeam;
                player.Color = Team.None.ColorHex;
                player.SetScore(0);
                player.RedirectToClassSelection();
                continue;
            }

            if (int.IsEvenInteger(index))
            {
                firstTeam.Members.Add(player);
                playerInfo.SetTeam(firstTeam.Id);
                var message = Smart.Format(Messages.AssignedToTeam, new { firstTeam.Name });
                player.SendClientMessage(firstTeam.ColorHex, message);
            }
            else
            {
                secondTeam.Members.Add(player);
                playerInfo.SetTeam(secondTeam.Id);
                var message = Smart.Format(Messages.AssignedToTeam, new { secondTeam.Name });
                player.SendClientMessage(secondTeam.ColorHex, message);
            }

            onPlayerAssigned.Invoke(player, playerInfo);
        }

        teamTextDrawRenderer.UpdateTeamScore(firstTeam);
        teamTextDrawRenderer.UpdateTeamScore(secondTeam);
        teamTextDrawRenderer.UpdateTeamMembers(firstTeam);
        teamTextDrawRenderer.UpdateTeamMembers(secondTeam);
    }
}
