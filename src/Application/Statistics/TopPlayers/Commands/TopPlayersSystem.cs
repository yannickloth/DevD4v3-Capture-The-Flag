namespace CTF.Application.Statistics.CommandInfrastructureNsNs2;

/// <summary>
/// Provides the top-players leaderboard commands.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): dialogService -> CD-33; topPlayersRepository -> CD-20. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.Repository, ChangeDriver.CommandSet, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class TopPlayersSystem(
    IDialogService dialogService,
    ITopPlayersRepository topPlayersRepository) : ISystem
{
    /// <summary>Shows the top players ranked by total kills.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Repository, ChangeDriver.Configuration, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.ClientMessage)]
    [PlayerCommand("topkills")]
    public void ShowByTotalKills(Player currentPlayer, int maxPlayers = 10)
    {
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(maxPlayers);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        var maxTopPlayers = result.Value;
        var columnHeaders = new[]
        {
            "Name",
            "Total Kills",
            "Rank"
        };
        var tablistDialog = new TablistDialog(
            caption: "Top Players By Total Kills",
            button1: "Close",
            button2: null,
            columnHeaders);

        var players = topPlayersRepository.GetByTotalKills(maxTopPlayers);
        foreach (TopPlayersByTotalKills player in players) 
        {
            var columns = new[]
            {
                player.PlayerName, 
                player.TotalKills.ToString(), 
                player.Rank.ToString()
            };
            tablistDialog.Add(columns);
        }
        dialogService.ShowAsync(currentPlayer, tablistDialog);
    }

    /// <summary>Shows the top players ranked by maximum killing spree.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Repository, ChangeDriver.Configuration, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.ClientMessage)]
    [PlayerCommand("topspree")]
    public void ShowByMaxKillingSpree(Player currentPlayer, int maxPlayers = 10)
    {
        Result<MaxTopPlayers> result = MaxTopPlayers.Create(maxPlayers);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        var maxTopPlayers = result.Value;
        var columnHeaders = new[]
        {
            "Name",
            "Killing Spree"
        };
        var tablistDialog = new TablistDialog(
            caption: "Top Players By Maximum Killing Spree",
            button1: "Close",
            button2: null,
            columnHeaders);

        var players = topPlayersRepository.GetByMaxKillingSpree(maxTopPlayers);
        foreach (TopPlayersByMaxKillingSpree player in players)
        {
            var columns = new[]
            {
                player.PlayerName,
                player.MaxKillingSpree.ToString()
            };
            tablistDialog.Add(columns);
        }
        dialogService.ShowAsync(currentPlayer, tablistDialog);
    }
}
