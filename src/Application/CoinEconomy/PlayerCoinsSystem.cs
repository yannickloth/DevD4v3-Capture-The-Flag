namespace CTF.Application.CoinEconomy;

/// <remarks>Change drivers: CD-06 (root; coin economy); CD-15 (command set) → CD-06; CD-09 (authorization policy) → CD-06; CD-17 (game configuration/.env schema) → CD-06; CD-10 (player-statistics/rank model: stats textdraw refresh) → CD-06; CD-01 (open.mp/SampSharp platform API) → CD-06</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-01; worldService -> CD-01; playerStatsRenderer -> CD-10; unixTimeSeconds -> CD-01; commandCooldowns -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerCoinsSystem(
    IEntityManager entityManager,
    IWorldService worldService,
    PlayerStatsRenderer playerStatsRenderer,
    UnixTimeSeconds unixTimeSeconds,
    CommandCooldowns commandCooldowns) : ISystem
{
    /// <remarks>Change drivers: CD-06 (root; coin economy); CD-09 (authorization policy) → CD-06; CD-10 (player-statistics/rank model: stats textdraw refresh) → CD-06; CD-01 (open.mp/SampSharp platform API) → CD-06; CD-15 (command set) → CD-06</remarks>
    [PlayerCommand("addcoins")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void AddCoinsToPlayer(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        int coins)
    {
        PlayerInfo targetPlayerInfo = targetPlayer.GetRequiredInfo();
            Result result = targetPlayerInfo.Stats.PerRound.AddCoins(coins);
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

    /// <remarks>Change drivers: CD-06 (root; coin economy); CD-09 (authorization policy) → CD-06; CD-10 (player-statistics/rank model: stats textdraw refresh) → CD-06; CD-01 (open.mp/SampSharp platform API) → CD-06; CD-15 (command set) → CD-06</remarks>
    [PlayerCommand("addallcoins")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void AddCoinsToAllPlayers(Player currentPlayer, int coins)
    {
        IEnumerable<Player> players = entityManager.GetComponents<Player>();
        foreach (Player targetPlayer in players)
        {
            PlayerInfo targetPlayerInfo = targetPlayer.GetRequiredInfo();
        Result result = targetPlayerInfo.Stats.PerRound.AddCoins(coins);
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

    /// <remarks>Change drivers: CD-06 (root; coin economy); CD-09 (authorization policy) → CD-06; CD-17 (game configuration/.env schema) → CD-06; CD-10 (player-statistics/rank model: stats textdraw refresh) → CD-06; CD-01 (open.mp/SampSharp platform API) → CD-06; CD-15 (command set) → CD-06</remarks>
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
        currentPlayerInfo.Stats.PerRound.AddCoins(100);
        playerStatsRenderer.UpdateTextDraw(currentPlayer);
        currentPlayer.SendClientMessage(Color.Yellow, Messages.GiveMeCoins);
    }

    /// <remarks>Change drivers: CD-06 (root; coin economy); CD-01 (open.mp/SampSharp platform API: OnPlayerConnect) → CD-06</remarks>
    [Event]
    public void OnPlayerConnect(Player player)
        => player.AddComponent<WaitTimeComponent>();

    /// <remarks>Change drivers: CD-06 (root; coin economy: coin-reward cooldown); CD-17 (game configuration/.env schema) → CD-06; CD-01 (open.mp/SampSharp platform API) → CD-06</remarks>
    private class WaitTimeComponent : Component
    {
        /// <remarks>Change drivers: CD-06 (root; coin economy: coin-reward cooldown); CD-17 (game configuration/.env schema) → CD-06; CD-01 (open.mp/SampSharp platform API) → CD-06</remarks>
        public long Value { get; set; }
    }
}