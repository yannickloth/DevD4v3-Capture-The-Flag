namespace CTF.Application.Statistics.ClientMessageNsNs3;

/// <summary>
/// Promotes the player to the next rank and grants the rank-up award.
/// Uniform flow: the statistics/rank model (CD-10) orchestration plus its engaged
/// authorization (CD-09), GunGame gate (CD-07), repository (CD-20), coin (CD-06),
/// player (CD-31), GameText (CD-35) and client-message (CD-36) contracts.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; gunGameMode -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.GunGame, ChangeDriver.Repository, ChangeDriver.Coin, ChangeDriver.Player, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
public class PlayerRankUpdater(
    IPlayerRepository playerRepository,
    IGunGameMode gunGameMode)
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.GunGame, ChangeDriver.Repository, ChangeDriver.Coin, ChangeDriver.Player, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
    public void Update(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (!RankCollection.CanMoveUpToNextRank(playerInfo))
            return;

        Rank nextRank = RankCollection.GetNextRank(playerInfo.Stats.RankId).Value;
        playerInfo.Stats.SetRank(nextRank.Id);
        playerRepository.UpdateRank(playerInfo);
        player.SendClientMessage(Color.Yellow, Smart.Format(Messages.NextRank, nextRank));

        if (nextRank.IsMax())
        {
            var message = Smart.Format(Messages.PromotedToRole, new { RoleName = RoleId.VIP });
            player.GameText(message, TimeSpan.FromSeconds(4), GameTextStyle.Style3);
            player.SendClientMessage(Color.Orange, message);
            playerInfo.Role.Set(RoleId.VIP);
            playerRepository.UpdateRole(playerInfo);
        }

        if (gunGameMode.IsEnabled)
            return;

        player.Armour = PlayerRankUpRewards.EarnedArmour;
        player.Health = PlayerRankUpRewards.EarnedHealth;
        playerInfo.Coins.AddCoins(PlayerRankUpRewards.EarnedCoins);

        var rankUpAwardSummary = Smart.Format(Messages.RankUpAwardSummary, new
        {
            Health = PlayerRankUpRewards.EarnedHealth,
            Armour = PlayerRankUpRewards.EarnedArmour,
            Coins  = PlayerRankUpRewards.EarnedCoins
        });

        player.SendClientMessage(Color.Orange, Messages.RankUpAwardGranted);
        player.SendClientMessage(Color.Orange, rankUpAwardSummary);
    }
}
