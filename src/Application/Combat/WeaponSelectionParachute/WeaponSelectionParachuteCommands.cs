namespace CTF.Application.Combat.CommandInfrastructureNsNs2;

/// <summary>
/// Gives a parachute to the player via the <c>/p</c> command.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Player, ChangeDriver.CommandInfrastructure)]
public class WeaponSelectionParachuteCommands : ISystem
{
    [PlayerCommand("p")]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.CommandSet)]
    public void GiveParachute(Player player)
    {
        player.GiveWeapon(Weapon.Parachute, 1);
    }
}
