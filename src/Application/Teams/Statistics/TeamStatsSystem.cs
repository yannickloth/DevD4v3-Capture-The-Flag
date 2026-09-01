namespace CTF.Application.Teams.Statistics;

/// <summary>
/// Tracks team statistics and provides stat-related commands.
/// </summary>
/// <remarks>Change drivers: CD-10 (player-statistics/rank model: team stats), CD-01 (open.mp/SampSharp platform API: player events, dialog, textdraw), CD-15 (command set: rstats/tstats commands), CD-09 (authorization policy: moderator gating), CD-02 (CTF game-rules specification: scoring).</remarks>
public class TeamStatsSystem(
    IDialogService dialogService, 
    IWorldService worldService,
    TeamTextDrawRenderer teamTextDrawRenderer) : ISystem
{
    /// <summary>Shows team textdraws when the player spawns.</summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: OnPlayerSpawn, textdraws).</remarks>
    [Event]
    public void OnPlayerSpawn(Player player)
    {
        teamTextDrawRenderer.Show(player);
    }

    /// <summary>Updates team kills and deaths on player death.</summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: OnPlayerDeath), CD-10 (player-statistics/rank model: team stats).</remarks>
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        PlayerInfo victimInfo = victim.GetRequiredInfo();
        victimInfo.Team.StatsPerRound.AddDeaths();

        if (killer.IsInvalidPlayer())
            return;

        PlayerInfo killerInfo = killer.GetRequiredInfo();
        killerInfo.Team.StatsPerRound.AddKills();
    }

    /// <summary>Resets team stats via the rstats command.</summary>
    /// <remarks>Change drivers: CD-15 (command set: rstats command), CD-09 (authorization policy: moderator gating), CD-10 (player-statistics/rank model: team stats), CD-01 (open.mp/SampSharp platform API: textdraw).</remarks>
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

    /// <summary>Shows team stats via the tstats command.</summary>
    /// <remarks>Change drivers: CD-15 (command set: tstats command), CD-01 (open.mp/SampSharp platform API: dialog), CD-10 (player-statistics/rank model: team stats).</remarks>
    [PlayerCommand("tstats")]
    public void ShowStats(Player player)
    {
        Team alphaTeam = Team.Alpha;
        Team betaTeam = Team.Beta;
        var content =
        $"""
        {alphaTeam.ColorHex}>>> Alpha Team: 
        Members: {alphaTeam.Members.Count}
        Score: {alphaTeam.StatsPerRound.Score}
        Kills: {alphaTeam.StatsPerRound.Kills}
        Deaths: {alphaTeam.StatsPerRound.Deaths}
        Flag captured by: {alphaTeam.Flag.CarrierName}

        {betaTeam.ColorHex}>>> Beta Team: 
        Members: {betaTeam.Members.Count}
        Score: {betaTeam.StatsPerRound.Score}
        Kills: {betaTeam.StatsPerRound.Kills}
        Deaths: {betaTeam.StatsPerRound.Deaths}
        Flag captured by: {betaTeam.Flag.CarrierName}
        """;
        var dialog = new MessageDialog("Team Stats", content, "Close");
        dialogService.ShowAsync(player, dialog);
    }
}
