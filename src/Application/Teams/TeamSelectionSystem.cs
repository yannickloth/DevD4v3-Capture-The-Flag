namespace CTF.Application.Teams;

/// <summary>
/// Handles team selection for players via the team command and dialog.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balancing); CD-01 (open.mp/SampSharp platform API: dialog, commands, player team/spawn) → CD-02; CD-15 (command set: team command) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; dialogService -> CD-01; teamTextDrawRenderer -> CD-29+CD-01. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class TeamSelectionSystem(
    IWorldService worldService,
    IDialogService dialogService,
    TeamTextDrawRenderer teamTextDrawRenderer) : ISystem
{
    /// <summary>Represents the handler for a team change.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
    public delegate void TeamChangeEventHandler(Player player, Team selectedTeam);

    /// <summary>Occurs when a player changes teams.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
    public event TeamChangeEventHandler TeamChangeEvent;

    /// <summary>Shows the team selection dialog.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balancing); CD-15 (command set: team command) → CD-02; CD-01 (open.mp/SampSharp platform API: dialog) → CD-02</remarks>
    [PlayerCommand("team")]
    public async Task ShowTeams(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Team == Team.None)
        {
            player.SendClientMessage(Color.Red, Messages.NoTeam);
            return;
        }

        var columnHeaders = new[]
        {
            "Name",
            "Members"
        };

        var tablistDialog = new TablistDialog(
            caption: "Select a team", 
            button1: "Select", 
            button2: "Close", 
            columnHeaders);

        Team alphaTeam = Team.Alpha;
        tablistDialog.Add(columns:
        [
            $"{alphaTeam.ColorHex}{alphaTeam.Name}",
            $"{alphaTeam.ColorHex}{alphaTeam.Members.Count}"
        ], tag: alphaTeam);

        Team betaTeam = Team.Beta;
        tablistDialog.Add(columns:
        [
            $"{betaTeam.ColorHex}{betaTeam.Name}",
            $"{betaTeam.ColorHex}{betaTeam.Members.Count}"
        ], tag: betaTeam);

        TablistDialogResponse response = await dialogService.ShowAsync(player, tablistDialog);
        if (response.IsRightButtonOrDisconnected())
            return;

        Team selectedTeam = response.Item.Tag as Team;
        ChangeTeam(player, selectedTeam);
    }

    private void ChangeTeam(Player player, Team selectedTeam)
    {
        Team alphaTeam = Team.Alpha;
        Team betaTeam = Team.Beta;
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (playerInfo.Team == selectedTeam)
        {
            player.SendClientMessage(Color.Red, Messages.PlayerIsAlreadyInTeam);
            return;
        }

        if (alphaTeam.Members.Count == betaTeam.Members.Count)
        {
            player.SendClientMessage(Color.Red, Messages.TeamsAreEqualInMembers);
            return;
        }

        if (selectedTeam.IsFull())
        {
            player.SendClientMessage(Color.Red, Messages.TeamIsFull);
            return;
        }

        TeamChangeEvent?.Invoke(player, selectedTeam);

        Team rivalTeam = selectedTeam.RivalTeam;
        selectedTeam.Members.Add(player);
        rivalTeam.Members.Remove(player);
        teamTextDrawRenderer.UpdateTeamMembers(selectedTeam);
        teamTextDrawRenderer.UpdateTeamMembers(rivalTeam);
        var message = Smart.Format(Messages.PlayerHasChangedTeams, new
        {
            PlayerName = player.Name,
            TeamName = selectedTeam.Name
        });
        worldService.SendClientMessage(selectedTeam.ColorHex, message);
        playerInfo.SetTeam(selectedTeam.Id);
        player.Team = (int)selectedTeam.Id;
        player.Spawn();
    }
}
