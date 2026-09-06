namespace CTF.Application.Statistics.Score.System.ShowStatsCommands;

/// <summary>
/// Shows a player's statistics in a dialog.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.CommandSet)]
public class PlayerStatsDialogCommands(
    IDialogService dialogService) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.CommandSet)]
    [PlayerCommand("mystats")]
    public void ShowStats(Player player)
    {
        var content = GetPlayerContent(player);
        var dialog = new MessageDialog($"Stats: {player.Name}", content, "Close");
        dialogService.ShowAsync(player, dialog);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.CommandSet)]
    [PlayerCommand("stats")]
    public void ShowStats(Player currentPlayer, [CommandParameter(Name = "playerId")]Player targetPlayer)
    {
        var content = GetPlayerContent(targetPlayer);
        var dialog = new MessageDialog($"Stats: {targetPlayer.Name}", content, "Close");
        dialogService.ShowAsync(currentPlayer, dialog);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Account)]
    private static string GetPlayerContent(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        string createdAt = player.IsUnauthenticated() ? 
            "None" : 
            playerInfo.Account.CreatedAt.ToIsoDateString();

        var content =
        $"""
        Current Team: {playerInfo.Appearance.Team.Name}
        Score for Round: {player.Score}
        Kills for Round: {playerInfo.Stats.PerRound.Kills}
        Deaths for Round: {playerInfo.Stats.PerRound.Deaths}
        Killing Spree for Round: {playerInfo.Stats.PerRound.KillingSpree}
        Coins: {playerInfo.Stats.PerRound.Coins}/100
        Max Killing Spree: {playerInfo.Stats.MaxKillingSpree}
        Total Kills: {playerInfo.Stats.TotalKills}
        Total Deaths: {playerInfo.Stats.TotalDeaths}
        Brought Flags: {playerInfo.Stats.BroughtFlags}
        Captured Flags: {playerInfo.Stats.CapturedFlags}
        Dropped Flags: {playerInfo.Stats.DroppedFlags}
        Returned Flags: {playerInfo.Stats.ReturnedFlags}
        HeadShots: {playerInfo.Stats.HeadShots}
        GunGame Wins: {playerInfo.Stats.GunGameWins}
        Role: {playerInfo.Role.Id}
        Rank: {playerInfo.Stats.RankId}
        Registration Date: {createdAt}
        """;
        return content;
    }
}
