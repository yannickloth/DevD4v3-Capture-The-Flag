namespace CTF.Application.Combat.HealthSystems;

/// <summary>
/// Provides the health-related commands.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; entityManager -> CD-32; unixTimeSeconds -> CD-41; commandCooldowns -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class HealthSystem(
    IWorldService worldService,
    IEntityManager entityManager,
    UnixTimeSeconds unixTimeSeconds,
    CommandCooldowns commandCooldowns) : ISystem
{
    /// <summary>Adds health to a target player.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Player)]
    [PlayerCommand("addhealth")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void AddHealthToPlayer(
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
            var message = Smart.Format(Messages.AddHealthToPlayer, new
            {
                PlayerName = targetPlayer.Name,
                Health = amount
            });
            currentPlayer.SendClientMessage(Color.Yellow, message);
        }

        {
            var message = Smart.Format(Messages.ReceiveHealthFromPlayer, new
            {
                PlayerName = currentPlayer.Name,
                Health = amount
            });
            targetPlayer.SendClientMessage(Color.Yellow, message);
            targetPlayer.AddHealth(amount);
        }
    }

    /// <summary>Adds health to all connected players.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.Player)]
    [PlayerCommand("addallhealth")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void AddHealthToAllPlayers(Player currentPlayer, float amount)
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
            targetPlayer.AddHealth(amount);
        }

        var message = Smart.Format(Messages.AddHealthToAllPlayers, new
        {
            PlayerName = currentPlayer.Name,
            Health = amount
        });
        worldService.SendClientMessage(Color.Yellow, message);
    }

    /// <summary>Restores a player's health, subject to a cooldown.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs, ChangeDriver.Player)]
    [PlayerCommand("health")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void RestoreHealth(Player currentPlayer)
    {
        var waitTimeComponent = currentPlayer.GetComponent<WaitTimeComponent>();
        if (waitTimeComponent.Value > unixTimeSeconds.Value)
        {
            var message = Smart.Format(Messages.TimeRequiredToReuseCommand, new 
            { 
                Minutes = commandCooldowns.Health
            });
            currentPlayer.SendClientMessage(Color.Red, message);
            return;
        }

        static int ConvertMinutesToSeconds(int value) => value * 60;
        int seconds = ConvertMinutesToSeconds(commandCooldowns.Health);
        waitTimeComponent.Value = unixTimeSeconds.Value + seconds;
        currentPlayer.Health = 100;
    }

    /// <summary>Adds the wait-time component when a player connects.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
    [Event]
    public void OnPlayerConnect(Player player)
        => player.AddComponent<WaitTimeComponent>();

    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Ecs)]
    private class WaitTimeComponent : Component
    {
        [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Ecs)]
        public long Value { get; set; }
    }
}
