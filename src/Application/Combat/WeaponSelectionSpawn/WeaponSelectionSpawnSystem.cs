namespace CTF.Application.Combat.WeaponSelectionSpawn;

/// <remarks>Injected dependencies (change drivers of these elements): gunGameMode -> CD-07. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.GunGame, ChangeDriver.Player)]
public class WeaponSelectionSpawnSystem(IGunGameMode gunGameMode) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.GunGame, ChangeDriver.Player)]
    public void OnPlayerSpawn(Player player)
    {
        if (gunGameMode.IsEnabled)
            return;

        var weaponSelection = player.GetComponent<WeaponSelectionComponent>();
        WeaponPack selectedWeapons = weaponSelection.SelectedWeapons;
        // Don't use foreach for performance reasons.
        // OnPlayerSpawn is invoked too often.
        for (int i = 0; i < selectedWeapons.TotalItems; i++)
        {
            IWeapon weapon = selectedWeapons[i];
            player.GiveWeapon(weapon.Id, IWeapon.UnlimitedAmmo);
        }
    }
}
