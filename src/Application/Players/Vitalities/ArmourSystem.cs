namespace CTF.Application.Players.Vitalities;

/// <summary>
/// Provides the armour-related commands.
/// </summary>
/// <remarks>Change drivers: CD-15 (command set), CD-03 (combat/weapon-rules specification), CD-17 (game configuration/.env schema), CD-01 (open.mp/SampSharp platform API)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; entityManager -> CD-01; unixTimeSeconds -> CD-01; commandCooldowns -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class ArmourSystem(
    IWorldService worldService,
    IEntityManager entityManager,
    UnixTimeSeconds unixTimeSeconds,
    CommandCooldowns commandCooldowns) : ISystem
{
    /// <summary>Adds armour to a target player.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy), CD-15 (command set), CD-03 (combat/weapon-rules specification), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("addarmour")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void AddArmourToPlayer(
        Player currentPlayer,
        [CommandParameter(Name = "playerId")]Player targetPlayer,
        float amount)
    {
        Result<Vitality> result = Vitality.Create(amount);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        {
            var message = Smart.Format(Messages.AddArmourToPlayer, new
            {
                PlayerName = targetPlayer.Name,
                Armour = amount
            });
            currentPlayer.SendClientMessage(Color.Yellow, message);
        }

        {
            var message = Smart.Format(Messages.ReceiveArmourFromPlayer, new
            {
                PlayerName = currentPlayer.Name,
                Armour = amount
            });
            targetPlayer.SendClientMessage(Color.Yellow, message);
            targetPlayer.AddArmour(amount);
        }
    }

    /// <summary>Adds armour to all connected players.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy), CD-15 (command set), CD-03 (combat/weapon-rules specification), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("addallarmour")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void AddArmourToAllPlayers(Player currentPlayer, float amount)
    {
        Result<Vitality> result = Vitality.Create(amount);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        IEnumerable<Player> players = entityManager.GetComponents<Player>();
        foreach (Player targetPlayer in players)
        {
            targetPlayer.AddArmour(amount);
        }

        var message = Smart.Format(Messages.AddArmourToAllPlayers, new
        {
            PlayerName = currentPlayer.Name,
            Armour = amount
        });
        worldService.SendClientMessage(Color.Yellow, message);
    }

    /// <summary>Restores a player's armour, subject to a cooldown.</summary>
    /// <remarks>Change drivers: CD-09 (authorization policy), CD-15 (command set), CD-03 (combat/weapon-rules specification), CD-17 (game configuration/.env schema), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("armour")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void RestoreArmour(Player currentPlayer)
    {
        var waitTimeComponent = currentPlayer.GetComponent<WaitTimeComponent>();
        if (waitTimeComponent.Value > unixTimeSeconds.Value)
        {
            var message = Smart.Format(Messages.TimeRequiredToReuseCommand, new 
            { 
                Minutes = commandCooldowns.Armour
            });
            currentPlayer.SendClientMessage(Color.Red, message);
            return;
        }

        static int ConvertMinutesToSeconds(int value) => value * 60;
        int seconds = ConvertMinutesToSeconds(commandCooldowns.Armour);
        waitTimeComponent.Value = unixTimeSeconds.Value + seconds;
        currentPlayer.Armour = 100;
    }

    /// <summary>Adds the wait-time component when a player connects.</summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API)</remarks>
    [Event]
    public void OnPlayerConnect(Player player)
        => player.AddComponent<WaitTimeComponent>();

    private class WaitTimeComponent : Component
    {
        public long Value { get; set; }
    }
}
