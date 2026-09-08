namespace CTF.Application.Combat.PlayerNsNs5;

/// <summary>
/// Adds health or armour to all connected players.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.Player)]
public class VitalityToAllCommands(
    IEntityManager entityManager,
    IWorldService worldService) : ISystem
{
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

    /// <summary>Adds armour to all connected players.</summary>
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.CommandInfrastructure, ChangeDriver.Ecs, ChangeDriver.ClientMessage, ChangeDriver.Player)]
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
}
