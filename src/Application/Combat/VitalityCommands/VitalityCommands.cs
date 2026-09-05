namespace CTF.Application.Combat.VitalityCommands;

/// <summary>
/// Adds health or armour to a target player.
/// </summary>
/// <remarks>No injected services (change drivers of these elements): each command mutates a single target Player through the AddHealth/AddArmour extensions + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Player)]
public class VitalityCommands : ISystem
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

    /// <summary>Adds armour to a target player.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.ClientMessage, ChangeDriver.Player)]
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
}
