namespace CTF.Application.Statistics.Score.System.ResetPlayerStatsCommands;

/// <summary>
/// Resets the current round kills/deaths of a player.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
public class ResetPlayerStatsCommands(
    IWorldService worldService,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
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
}
