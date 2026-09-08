namespace CTF.Application.Statistics.ClientMessageNsNs2;

/// <summary>
/// Updates the player's per-round killing spree and grants the spree rewards.
/// Uniform flow: the statistics/rank model (CD-10) orchestration plus its engaged
/// coin (CD-06), GunGame gate (CD-07), repository (CD-20), player (CD-31), ECS (CD-32),
/// GameText (CD-35) and client-message (CD-36) contracts.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; playerRepository -> CD-20; gunGameMode -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin, ChangeDriver.GunGame, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
public class PlayerKillingSpreeUpdater(
    IWorldService worldService,
    IPlayerRepository playerRepository,
    IGunGameMode gunGameMode)
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin, ChangeDriver.GunGame, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
    public void Update(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.PerRound.AddKillingSpree();
        int currentKillingSpree = playerInfo.Stats.PerRound.KillingSpree;

        if (currentKillingSpree < PlayerKillingSpreeRewards.MinimumKillingSpree)
            return;

        if (PlayerKillingSpreeRewards.HasSurpassedMaxKillingSpree(playerInfo))
        {
            playerInfo.Stats.SetMaxKillingSpree(currentKillingSpree);
            playerRepository.UpdateMaxKillingSpree(playerInfo);
        }

        if (gunGameMode.IsEnabled)
            return;

        player.GameText($"KILL X{currentKillingSpree}", TimeSpan.FromSeconds(3), GameTextStyle.Style3);
        playerInfo.Coins.AddCoins(PlayerKillingSpreeRewards.EarnedCoins);
        player.AddHealth(PlayerKillingSpreeRewards.EarnedHealth);

        if (currentKillingSpree % 3 == 0)
        {
            var message = Smart.Format(Messages.ConsecutiveKills, new
            {
                PlayerName = player.Name,
                Kills = currentKillingSpree
            });

            // Sample Message:
            // Dave has had 3 consecutive kills without dying.
            worldService.SendClientMessage(Color.Orange, message);
            player.AddHealth(PlayerKillingSpreeRewards.ConsecutiveKillsBonusHealth);
        }
    }
}
