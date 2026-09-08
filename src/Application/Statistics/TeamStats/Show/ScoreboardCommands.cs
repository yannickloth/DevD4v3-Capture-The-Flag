namespace CTF.Application.Statistics.DialogNsNs3;

/// <summary>
/// Shows the team scoreboard dialog to players.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): dialogService -> CD-33. Driven by the IDialogService (platform) contract + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Dialog)]
public class TeamScoreboardCommands(IDialogService dialogService) : ISystem
{
    /// <summary>Shows the team scoreboard dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    [PlayerCommand("scoreboard")]
    public void ShowPlayers(Player player)
    {
        Team alphaTeam = Team.Alpha;
        Team betaTeam = Team.Beta;

        var caption =
            $"{alphaTeam.ColorHex}Alpha Score: {alphaTeam.StatsPerRound.Score} " +
            $"{betaTeam.ColorHex}Beta Score: {betaTeam.StatsPerRound.Score}";

        var columnHeaders = new[]
        {
            "Name",
            "Score",
            "Kills",
            "Deaths"
        };

        var tablistDialog = new TablistDialog(
            caption,
            button1: "Close",
            button2: default,
            columnHeaders);

        var alphaTeamMembers = alphaTeam
            .Members
            .OrderByDescending(player => player.Score);

        foreach (Player teamMember in alphaTeamMembers)
        {
            PlayerInfo teamMemberInfo = teamMember.GetRequiredInfo();
            string[] columns =
            [
                $"{alphaTeam.ColorHex}{teamMember.Name}",
                $"{alphaTeam.ColorHex}{teamMember.Score}",
                $"{alphaTeam.ColorHex}{teamMemberInfo.Stats.PerRound.Kills}",
                $"{alphaTeam.ColorHex}{teamMemberInfo.Stats.PerRound.Deaths}"
            ];
            tablistDialog.Add(columns);
        }

        var betaTeamMembers = betaTeam
            .Members
            .OrderByDescending(player => player.Score);

        foreach (Player teamMember in betaTeamMembers)
        {
            PlayerInfo teamMemberInfo = teamMember.GetRequiredInfo();
            string[] columns =
            [
                $"{betaTeam.ColorHex}{teamMember.Name}",
                $"{betaTeam.ColorHex}{teamMember.Score}",
                $"{betaTeam.ColorHex}{teamMemberInfo.Stats.PerRound.Kills}",
                $"{betaTeam.ColorHex}{teamMemberInfo.Stats.PerRound.Deaths}"
            ];
            tablistDialog.Add(columns);
        }

        dialogService.ShowAsync(player, tablistDialog);
    }
}
