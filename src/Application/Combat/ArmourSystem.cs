namespace CTF.Application.Combat;

/// <summary>
/// Provides the armour-related commands.
/// </summary>
/// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification); CD-15 (command set) → CD-03; CD-09 (authorization policy) → CD-03; CD-17 (game configuration/.env schema) → CD-03; CD-31 (player events & state); CD-32 (ECS runtime); CD-36 (client messages); CD-43 (command infrastructure) → CD-03</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; entityManager -> CD-32; unixTimeSeconds -> CD-41; commandCooldowns -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class ArmourSystem(
    IWorldService worldService,
    IEntityManager entityManager,
    UnixTimeSeconds unixTimeSeconds,
    CommandCooldowns commandCooldowns) : ISystem
{
    /// <summary>Adds armour to a target player.</summary>
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification); CD-15 (command set) → CD-03; CD-09 (authorization policy) → CD-03; CD-43 (command infrastructure); CD-36 (client messages); CD-31 (AddArmour) → CD-03</remarks>
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
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification); CD-15 (command set) → CD-03; CD-09 (authorization policy) → CD-03; CD-43 (command infrastructure); CD-32 (ECS runtime); CD-36 (client messages); CD-31 (AddArmour) → CD-03</remarks>
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
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification); CD-15 (command set) → CD-03; CD-17 (game configuration/.env schema) → CD-03; CD-09 (authorization policy) → CD-03; CD-43 (command infrastructure); CD-36 (client messages); CD-32 (ECS runtime); CD-31 (armour state) → CD-03</remarks>
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
    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification); CD-31 (OnPlayerConnect) → CD-03</remarks>
    [Event]
    public void OnPlayerConnect(Player player)
        => player.AddComponent<WaitTimeComponent>();

    /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification: armour-restore cooldown); CD-15 (command set) → CD-03; CD-17 (game configuration/.env schema) → CD-03; CD-32 (component storage) → CD-03</remarks>
    private class WaitTimeComponent : Component
    {
        /// <remarks>Change drivers: CD-03 (root; combat/weapon-rules specification: armour-restore cooldown); CD-32 (component/tick time) → CD-03</remarks>
        public long Value { get; set; }
    }
}
