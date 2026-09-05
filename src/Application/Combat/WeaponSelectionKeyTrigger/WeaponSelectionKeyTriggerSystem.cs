namespace CTF.Application.Combat.WeaponSelectionKeyTrigger;

/// <summary>
/// Dispatches the weapon-selection dialog commands from key presses.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): dialog -> CD-21 (DI wiring). Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Dialog)]
public class WeaponSelectionKeyTriggerSystem(
    WeaponSelectionDialog dialog) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Dialog)]
    public async Task OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        bool parachuteCombo = KeyUtils.HasPressed(newKeys, oldKeys, Keys.Walk | Keys.CtrlBack);
        if (!parachuteCombo && KeyUtils.HasPressed(newKeys, oldKeys, Keys.Yes))
        {
            await dialog.ShowWeapons(player);
        }
        else if (!parachuteCombo && KeyUtils.HasPressed(newKeys, oldKeys, Keys.CtrlBack))
        {
            await dialog.ShowWeaponPackage(player);
        }
    }
}
