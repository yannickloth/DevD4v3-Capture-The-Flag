namespace CTF.Application.Combat.ClientMessageDomain;

/// <summary>
/// Shows the weapon-selection usage messages when a player requests a spawn.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): gunGameMode -> CD-07; dialog -> CD-21 (DI wiring). Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.GunGame, ChangeDriver.Player, ChangeDriver.ClientMessage)]
public class WeaponSelectionSpawnTriggerSystem(
    IGunGameMode gunGameMode,
    WeaponSelectionDialog dialog) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.GunGame, ChangeDriver.Player, ChangeDriver.ClientMessage)]
    public async Task OnPlayerRequestSpawn(Player player)
    {
        if (gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Orange, GunGameMessages.GunGameModeStarted);
            player.SendClientMessage(Color.Orange, GunGameMessages.GunGameModeObjective);
            return;
        }

        await dialog.ShowWeapons(player);
        player.SendClientMessage(Color.Orange, Messages.WeaponListUsage);
        player.SendClientMessage(Color.Orange, Messages.WeaponPackUsage);
    }
}
