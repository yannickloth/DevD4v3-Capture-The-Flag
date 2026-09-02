namespace CTF.Application.Players.Accounts.Statistics;

/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-09 (authorization policy) → CD-10; CD-07 (GunGame mode rules) → CD-10; CD-20 (outbound repository contract) → CD-10</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; gunGameMode -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerRankUpdater(
    IPlayerRepository playerRepository,
    IGunGameMode gunGameMode)
{
    private const int EarnedHealth = 100;
    private const int EarnedArmour = 100;
    private const int EarnedCoins  = 100;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-09 (authorization policy) → CD-10; CD-07 (GunGame mode rules) → CD-10; CD-20 (outbound repository contract) → CD-10</remarks>
    public void Update(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (!playerInfo.CanMoveUpToNextRank())
            return;

        IRank nextRank = RankCollection.GetNextRank(playerInfo.RankId).Value;
        playerInfo.SetRank(nextRank.Id);
        playerRepository.UpdateRank(playerInfo);
        player.SendClientMessage(Color.Yellow, Smart.Format(Messages.NextRank, nextRank));

        if (nextRank.IsMax())
        {
            var message = Smart.Format(Messages.PromotedToRole, new { RoleName = RoleId.VIP });
            player.GameText(message, TimeSpan.FromSeconds(4), GameTextStyle.Style3);
            player.SendClientMessage(Color.Orange, message);
            playerInfo.SetRole(RoleId.VIP);
            playerRepository.UpdateRole(playerInfo);
        }

        if (gunGameMode.IsEnabled)
            return;

        player.Armour = EarnedArmour;
        player.Health = EarnedHealth;
        playerInfo.StatsPerRound.AddCoins(EarnedCoins);

        var rankUpAwardSummary = Smart.Format(Messages.RankUpAwardSummary, new
        {
            Health = EarnedHealth,
            Armour = EarnedArmour,
            Coins  = EarnedCoins
        });

        player.SendClientMessage(Color.Orange, Messages.RankUpAwardGranted);
        player.SendClientMessage(Color.Orange, rankUpAwardSummary);
    }
}
