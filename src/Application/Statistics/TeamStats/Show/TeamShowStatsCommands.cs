namespace CTF.Application.Statistics.TeamStats.Show;

/// <summary>
/// Shows the team statistics to a player via a dialog.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Dialog)]
public class TeamShowStatsCommands(
    IDialogService dialogService) : ISystem
{
    /// <summary>Shows team stats via the tstats command.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Dialog)]
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
        Flag captured by: {alphaTeam.Flag.Carrier?.DisplayName ?? "None"}

        {betaTeam.ColorHex}>>> Beta Team: 
        Members: {betaTeam.Members.Count}
        Score: {betaTeam.StatsPerRound.Score}
        Kills: {betaTeam.StatsPerRound.Kills}
        Deaths: {betaTeam.StatsPerRound.Deaths}
        Flag captured by: {betaTeam.Flag.Carrier?.DisplayName ?? "None"}
        """;
        var dialog = new MessageDialog("Team Stats", content, "Close");
        dialogService.ShowAsync(player, dialog);
    }
}
