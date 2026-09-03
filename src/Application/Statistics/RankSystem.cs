namespace CTF.Application.Statistics;

/// <summary>
/// Provides the ranks command that displays the rank tiers.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
public class RankSystem : ISystem
{
    private readonly TablistDialog _tablistDialog;

    /// <summary>Builds the rank tiers dialog.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public RankSystem()
    {
        var columnHeaders = new[]
        {
            "Rank",
            "Total Required Kills"
        };
        _tablistDialog = new TablistDialog(
            caption: "Ranks",
            button1: "Close",
            button2: null,
            columnHeaders);

        var ranks = RankCollection.GetAll();
        foreach (Rank rank in ranks)
            _tablistDialog.Add(rank.Name, rank.RequiredKills.ToString());
    }

    /// <summary>Shows the ranks dialog to the player.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-15 (command set) → CD-10; CD-01 (open.mp/SampSharp platform API) → CD-10</remarks>
    [PlayerCommand("ranks")]
    public void ShowRanks(Player player, IDialogService dialogService)
    {
        dialogService.ShowAsync(player, _tablistDialog);
    }
}
