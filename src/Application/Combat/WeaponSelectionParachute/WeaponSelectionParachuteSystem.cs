namespace CTF.Application.Combat.WeaponSelectionParachute;

[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandSet, ChangeDriver.Player, ChangeDriver.CommandInfrastructure)]
public class WeaponSelectionParachuteSystem : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
    public void OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.Walk | Keys.CtrlBack))
        {
            GiveParachute(player);
        }
    }

    [PlayerCommand("p")]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.CommandInfrastructure, ChangeDriver.Player, ChangeDriver.CommandSet)]
    public void GiveParachute(Player player)
    {
        player.GiveWeapon(Weapon.Parachute, 1);
    }
}
