namespace CTF.Application.CoinEconomy.AddCoinsCommands;

/// <summary>
/// Adds coins to a single target player.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
public class AddCoinsCommands(
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Statistics, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    [PlayerCommand("addcoins")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void AddCoinsToPlayer(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        int coins)
    {
        PlayerInfo targetPlayerInfo = targetPlayer.GetRequiredInfo();
            Result result = targetPlayerInfo.Coins.AddCoins(coins);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        {
            var message = Smart.Format(Messages.AddCoinsToPlayer, new
            {
                Coins = coins,
                PlayerName = targetPlayer.Name
            });
            currentPlayer.SendClientMessage(Color.Yellow, message);
        }
        {
            var message = Smart.Format(Messages.ReceiveCoinsFromPlayer, new
            {
                Coins = coins,
                PlayerName = currentPlayer.Name
            });
            targetPlayer.SendClientMessage(Color.Yellow, message);
        }
        playerStatsRenderer.UpdateTextDraw(targetPlayer);
    }
}
