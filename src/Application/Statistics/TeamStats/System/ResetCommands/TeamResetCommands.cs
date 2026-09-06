namespace CTF.Application.Statistics.TeamStats.System.ResetCommands;

/// <summary>
/// Resets the team statistics for the current round.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; teamTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.TextDraw)]
public class TeamResetCommands(
    IWorldService worldService,
    TeamTextDrawRenderer teamTextDrawRenderer) : ISystem
{
    /// <summary>Resets team stats via the rstats command.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.TextDraw)]
    [PlayerCommand("rstats")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ResetStats(Player player) 
    {
        Team alphaTeam = Team.Alpha;
        Team betaTeam = Team.Beta;
        alphaTeam.StatsPerRound.Reset();
        betaTeam.StatsPerRound.Reset();
        teamTextDrawRenderer.UpdateTeamScore(alphaTeam);
        teamTextDrawRenderer.UpdateTeamScore(betaTeam);
        var message = Smart.Format(Messages.ResetTeamStats, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
    }
}
