namespace CTF.Application.Statistics.Ranks.Updater;

/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; gunGameMode -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.GunGame, ChangeDriver.Repository)]
public class PlayerRankUpdater(
    IPlayerRepository playerRepository,
    IGunGameMode gunGameMode)
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Combat)]
    private const int EarnedHealth = 100;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Combat)]
    private const int EarnedArmour = 100;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    private const int EarnedCoins  = 100;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Authorization, ChangeDriver.GunGame, ChangeDriver.Repository)]
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

        player.Armour = EarnedArmour;
        player.Health = EarnedHealth;
        playerInfo.Stats.PerRound.AddCoins(EarnedCoins);

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
