namespace CTF.Application.Players.Accounts.Statistics;

/// <remarks>Change drivers: CD-09 (authorization policy); CD-10 (player-statistics/rank model); CD-20 (outbound repository contract)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; playerStatsRenderer -> CD-29+CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerKillsSystem(
    IPlayerRepository playerRepository,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    /// <remarks>Change drivers: CD-09 (authorization policy); CD-10 (player-statistics/rank model); CD-20 (outbound repository contract)</remarks>
    [PlayerCommand("settotalkills")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void SetTotalKillsToPlayer(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        int kills)
    {
        if (targetPlayer.IsUnauthenticated())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.UnauthenticatedPlayer);
            return;
        }

        PlayerInfo targetPlayerInfo = targetPlayer.GetRequiredInfo();
        Result result = targetPlayerInfo.SetTotalKills(kills);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        IRank rank = RankCollection.GetByRequiredKills(kills).Value;
        if (rank.Id != targetPlayerInfo.RankId)
        {
            targetPlayerInfo.SetRank(rank.Id);
            playerRepository.UpdateRank(targetPlayerInfo);
            playerStatsRenderer.UpdateTextDraw(targetPlayer);
        }
        playerRepository.UpdateTotalKills(targetPlayerInfo);
        {
            var message = Smart.Format(Messages.SetKillsToPlayer, new
            {
                Kills = kills,
                PlayerName = targetPlayer.Name
            });
            currentPlayer.SendClientMessage(Color.Yellow, message);
        }
        {
            var message = Smart.Format(Messages.ReceiveKillsFromPlayer, new
            {
                Kills = kills,
                PlayerName = currentPlayer.Name
            });
            targetPlayer.SendClientMessage(Color.Yellow, message);
        }
    }
}
