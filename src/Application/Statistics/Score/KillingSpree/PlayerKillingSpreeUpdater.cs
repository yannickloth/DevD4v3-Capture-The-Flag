namespace CTF.Application.Statistics.Score.KillingSpree;

/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; playerRepository -> CD-20; gunGameMode -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin, ChangeDriver.GunGame, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
public class PlayerKillingSpreeUpdater(
    IWorldService worldService,
    IPlayerRepository playerRepository,
    IGunGameMode gunGameMode)
{
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    private const int MinimumKillingSpree = 2;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin)]
    private const int EarnedCoins = 20;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Combat)]
    private const int EarnedHealth = 10;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Combat)]
    private const int ConsecutiveKillsBonusHealth = 40;

    /// <summary>Determines whether the player has surpassed their previously recorded maximum killing spree.</summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics)]
    public static bool HasSurpassedMaxKillingSpree(PlayerInfo playerInfo)
        => playerInfo.Stats.PerRound.KillingSpree > playerInfo.Stats.MaxKillingSpree;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Coin, ChangeDriver.GunGame, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.GameText, ChangeDriver.ClientMessage)]
    public void Update(Player player)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.PerRound.AddKillingSpree();
        int currentKillingSpree = playerInfo.Stats.PerRound.KillingSpree;

        if (currentKillingSpree < MinimumKillingSpree)
            return;

        if (HasSurpassedMaxKillingSpree(playerInfo))
        {
            playerInfo.Stats.SetMaxKillingSpree(currentKillingSpree);
            playerRepository.UpdateMaxKillingSpree(playerInfo);
        }

        if (gunGameMode.IsEnabled)
            return;

        player.GameText($"KILL X{currentKillingSpree}", TimeSpan.FromSeconds(3), GameTextStyle.Style3);
        playerInfo.Coins.AddCoins(EarnedCoins);
        player.AddHealth(EarnedHealth);

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
            player.AddHealth(ConsecutiveKillsBonusHealth);
        }
    }
}
