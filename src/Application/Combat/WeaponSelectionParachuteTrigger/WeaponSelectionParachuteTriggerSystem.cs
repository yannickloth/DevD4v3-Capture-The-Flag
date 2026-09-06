namespace CTF.Application.Combat.WeaponSelectionParachuteTrigger;

/// <summary>
/// Gives a parachute to the player when the walk+secondary-fire key combo is pressed.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): parachuteCommands -> CD-21 (DI wiring). Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
public class WeaponSelectionParachuteTriggerSystem(
    WeaponSelectionParachuteCommands parachuteCommands) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player)]
    public void OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        if (KeyUtils.HasPressed(newKeys, oldKeys, Keys.Walk | Keys.CtrlBack))
        {
            parachuteCommands.GiveParachute(player);
        }
    }
}
