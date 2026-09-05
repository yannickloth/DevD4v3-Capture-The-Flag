namespace CTF.Application.Statistics.Score.System;

/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; dialogService -> CD-33; playerRepository -> CD-20; playerRankUpdater -> CD-10; killingSpreeUpdater -> CD-10; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Player, ChangeDriver.Dialog, ChangeDriver.TextDraw, ChangeDriver.CommandInfrastructure, ChangeDriver.Repository, ChangeDriver.Account)]
public class PlayerStatsSystem(
    IWorldService worldService,
    IDialogService dialogService,
    IPlayerRepository playerRepository,
    PlayerRankUpdater playerRankUpdater,
    PlayerKillingSpreeUpdater killingSpreeUpdater,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw)]
    [Event]
    public void OnPlayerConnect(Player player)
    {
        playerStatsRenderer.CreateTextDraw(player);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw)]
    [Event]
    public void OnPlayerSpawn(Player player)
    {
        playerStatsRenderer.UpdateTextDraw(player);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Account, ChangeDriver.Repository)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        if (player.IsUnauthenticated())
            return;

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.SetLastConnection();
        playerRepository.UpdateLastConnection(playerInfo);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.Repository)]
    [Event]
    public void OnPlayerDeath(Player victim, Player killer, Weapon reason)
    {
        PlayerInfo victimInfo = victim.GetRequiredInfo();
        victimInfo.Stats.PerRound.AddDeaths();
        victimInfo.Stats.PerRound.ResetKillingSpree();
        victimInfo.Stats.AddTotalDeaths();
        playerRepository.UpdateTotalDeaths(victimInfo);

        if (killer.IsInvalidPlayer())
            return;

        PlayerInfo killerInfo = killer.GetRequiredInfo();
        killerInfo.Stats.PerRound.AddKills();
        killerInfo.Stats.AddTotalKills();
        killer.AddScore();
        playerRepository.UpdateTotalKills(killerInfo);
        killingSpreeUpdater.Update(killer);
        playerRankUpdater.Update(killer);
        playerStatsRenderer.UpdateTextDraw(killer);
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Player, ChangeDriver.Dialog)]
    [Event]
    public void OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.AnalogRight))
        {
            ShowStats(player);
        }
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    [PlayerCommand("re")]
    public void ResetPlayerStats(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.PerRound.ResetKills();
        playerInfo.Stats.PerRound.ResetDeaths();
        player.SetScore(0);
        playerStatsRenderer.UpdateTextDraw(player);
        var message = Smart.Format(Messages.ResetPlayerStats, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
    }

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
