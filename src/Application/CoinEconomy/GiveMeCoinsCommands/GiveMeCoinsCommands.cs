
namespace CTF.Application.CoinEconomy.CommandSetNsNs4;

/// <summary>
/// Gives the current player coins, subject to a cooldown.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerStatsRenderer -> CD-10; unixTimeSeconds -> CD-41; commandCooldowns -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.Statistics, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
public class GiveMeCoinsCommands(
    UnixTimeSeconds unixTimeSeconds,
    CommandCooldowns commandCooldowns,
    PlayerStatsRenderer playerStatsRenderer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Coin, ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.Statistics, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet)]
    [PlayerCommand("givemecoins")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void GiveMeCoins(Player currentPlayer) 
    {
        var waitTimeComponent = currentPlayer.GetComponent<WaitTimeComponent>();
        if (waitTimeComponent.Value > unixTimeSeconds.Value)
        {
            var message = Smart.Format(Messages.TimeRequiredToReuseCommand, new 
            { 
                Minutes = commandCooldowns.Coins
            });
            currentPlayer.SendClientMessage(Color.Red, message);
            return;
        }

        static int ConvertMinutesToSeconds(int value) => value * 60;
        int seconds = ConvertMinutesToSeconds(commandCooldowns.Coins);
        waitTimeComponent.Value = unixTimeSeconds.Value + seconds;
        PlayerInfo currentPlayerInfo = currentPlayer.GetRequiredInfo();
        currentPlayerInfo.Coins.AddCoins(100);
        playerStatsRenderer.UpdateTextDraw(currentPlayer);
        currentPlayer.SendClientMessage(Color.Yellow, Messages.GiveMeCoins);
    }
}
