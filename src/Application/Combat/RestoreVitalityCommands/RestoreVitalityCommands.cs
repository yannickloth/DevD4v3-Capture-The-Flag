namespace CTF.Application.Combat.PlayerNsNs6;

/// <summary>
/// Restores a player's health or armour to full, subject to a cooldown.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): unixTimeSeconds -> CD-41; commandCooldowns -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs, ChangeDriver.Player)]
public class RestoreVitalityCommands(
    UnixTimeSeconds unixTimeSeconds,
    CommandCooldowns commandCooldowns) : ISystem
{
    /// <summary>Restores a player's health, subject to a cooldown.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs, ChangeDriver.Player)]
    [PlayerCommand("health")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void RestoreHealth(Player currentPlayer)
    {
        var healthCooldownComponent = currentPlayer.GetComponent<HealthCooldownComponent>();
        if (healthCooldownComponent.Value > unixTimeSeconds.Value)
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
        healthCooldownComponent.Value = unixTimeSeconds.Value + seconds;
        currentPlayer.Health = 100;
    }

    /// <summary>Restores a player's armour, subject to a cooldown.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Ecs, ChangeDriver.Player)]
    [PlayerCommand("armour")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void RestoreArmour(Player currentPlayer)
    {
        var armourCooldownComponent = currentPlayer.GetComponent<ArmourCooldownComponent>();
        if (armourCooldownComponent.Value > unixTimeSeconds.Value)
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
        armourCooldownComponent.Value = unixTimeSeconds.Value + seconds;
        currentPlayer.Armour = 100;
    }
}
