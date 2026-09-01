namespace CTF.Application.Players.Weapons;

/// <remarks>Change drivers: CD-04 (weapon-catalog configuration), CD-03 (combat/weapon-rules specification), CD-07 (GunGame mode rules), CD-15 (command set), CD-01 (open.mp/SampSharp platform API)</remarks>
public class WeaponCatalogSystem(
    IEntityManager entityManager,
    IDialogService dialogService,
    IGunGameMode gunGameMode,
    ActiveWeaponCatalog weaponCatalog,
    WeaponCatalogSettings weaponCatalogSettings) : ISystem
{
    [PlayerCommand("weaponcatalog")]
    [RequiresMinimumRole(RoleId.Admin)]
    /// <remarks>Change drivers: CD-04 (weapon-catalog configuration), CD-03 (combat/weapon-rules specification), CD-07 (GunGame mode rules), CD-15 (command set), CD-01 (open.mp/SampSharp platform API)</remarks>
    public async Task ShowCatalogs(Player player)
    {
        if (gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponCatalogUnavailable);
            return;
        }

        var dialog = new ListDialog("Weapon Catalogs", "Select", "Close");
        foreach (WeaponCatalogType type in Enum.GetValues<WeaponCatalogType>())
            dialog.Add(type.GetDisplayName());

        ListDialogResponse response = await dialogService.ShowAsync(player, dialog);
        if (response.IsRightButtonOrDisconnected())
            return;

        if (gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponCatalogUnavailable);
            return;
        }

        WeaponCatalogType selectedCatalog = (WeaponCatalogType)response.ItemIndex;
        if (weaponCatalogSettings.Type == selectedCatalog)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponCatalogAlreadyActive);
            return;
        }

        weaponCatalogSettings.Change(selectedCatalog);
        var catalogName = selectedCatalog.GetDisplayName();
        var message = Smart.Format(Messages.WeaponCatalogChangedTo, new { Name = catalogName });

        foreach (Player currentPlayer in entityManager.GetComponents<Player>())
        {
            var weaponSelection = currentPlayer.GetComponent<WeaponSelectionComponent>();
            WeaponPack selectedWeapons = weaponSelection.SelectedWeapons;

            // Ensure the player's weapon pack only contains weapons
            // available in the newly selected catalog.
            selectedWeapons.RemoveAll(weapon =>
            {
                bool shouldRemove = !weaponCatalog.Contains(weapon);

                if (shouldRemove)
                    currentPlayer.RemoveWeapon(weapon.Id);

                return shouldRemove;
            });

            currentPlayer.SendClientMessage(Color.Yellow, message);
        }
    }
}
