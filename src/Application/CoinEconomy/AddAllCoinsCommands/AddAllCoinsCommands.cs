namespace CTF.Application.CoinEconomy.CommandSetNsNs3;

/// <summary>
/// Adds coins to all connected players.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; worldService -> CD-36; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
public class AddAllCoinsCommands(
    IEntityManager entityManager,
    IWorldService worldService,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    [PlayerCommand("addallcoins")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void AddCoinsToAllPlayers(Player currentPlayer, int coins)
    {
        IEnumerable<Player> players = entityManager.GetComponents<Player>();
        foreach (Player targetPlayer in players)
        {
            PlayerInfo targetPlayerInfo = targetPlayer.GetRequiredInfo();
        Result result = targetPlayerInfo.Coins.AddCoins(coins);
            if (result.IsFailed)
            {
                currentPlayer.SendClientMessage(Color.Red, result.Message);
                return;
            }
            playerStatsRenderer.UpdateTextDraw(targetPlayer);
        }

        var message = Smart.Format(Messages.AddCoinsToAllPlayers, new
        {
            PlayerName = currentPlayer.Name,
            Coins = coins
        });
        worldService.SendClientMessage(Color.Yellow, message);
    }
}
