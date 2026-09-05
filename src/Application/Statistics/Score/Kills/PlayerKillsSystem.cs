namespace CTF.Application.Statistics.Score.Kills;

/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Repository)]
public class PlayerKillsSystem(
    IPlayerRepository playerRepository,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.Repository, ChangeDriver.CommandSet)]
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
        Result result = targetPlayerInfo.Stats.SetTotalKills(kills);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        Rank rank = RankCollection.GetByRequiredKills(kills).Value;
        if (rank.Id != targetPlayerInfo.Stats.RankId)
        {
            targetPlayerInfo.Stats.SetRank(rank.Id);
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
