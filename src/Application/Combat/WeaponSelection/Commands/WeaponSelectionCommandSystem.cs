namespace CTF.Application.Combat.WeaponSelection.Commands;

/// <remarks>Injected dependencies (change drivers of these elements): dialogService -> CD-33; gunGameMode -> CD-07; weaponCatalog -> CD-04. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.WeaponCatalog, ChangeDriver.GunGame, ChangeDriver.CommandSet, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class WeaponSelectionCommandSystem(
    IDialogService dialogService,
    IGunGameMode gunGameMode,
    ActiveWeaponCatalog weaponCatalog) : ISystem
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

        await ShowWeapons(player);
        player.SendClientMessage(Color.Orange, Messages.WeaponListUsage);
        player.SendClientMessage(Color.Orange, Messages.WeaponPackUsage);
    }

    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.Player, ChangeDriver.Dialog)]
    public async Task OnPlayerKeyStateChange(Player player, Keys newKeys, Keys oldKeys)
    {
        bool parachuteCombo = KeyUtils.HasPressed(newKeys, oldKeys, Keys.Walk | Keys.CtrlBack);
        if (!parachuteCombo && KeyUtils.HasPressed(newKeys, oldKeys, Keys.Yes))
        {
            await ShowWeapons(player);
        }
        else if (!parachuteCombo && KeyUtils.HasPressed(newKeys, oldKeys, Keys.CtrlBack))
        {
            await ShowWeaponPackage(player);
        }
    }

    [PlayerCommand("weapons")]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.WeaponCatalog, ChangeDriver.GunGame, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.Ecs, ChangeDriver.Player, ChangeDriver.CommandSet)]
    public async Task ShowWeapons(Player player)
    {
        if (gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponListUnavailable);
            return;
        }

        var dialog = new ListDialog("Select Weapons", "Select", "Close");
        var weapons = weaponCatalog.GetAll();
        foreach (IWeapon weapon in weapons)
            dialog.Add(weapon.Name);

        ListDialogResponse response = await dialogService.ShowAsync(player, dialog);
        if (response.IsRightButtonOrDisconnected())
            return;

        if (gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponListUnavailable);
            return;
        }

        Result<IWeapon> weaponResult = weaponCatalog.GetByName(response.Item.Text);
        if (weaponResult.IsFailed)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponCatalogChanged);
            return;
        }

        var weaponSelection = player.GetComponent<WeaponSelectionComponent>();
        WeaponPack selectedWeapons = weaponSelection.SelectedWeapons;
        IWeapon weaponSelectedFromDialog = weaponResult.Value;
        if (selectedWeapons.Exists(weaponSelectedFromDialog))
        {
            var message = Smart.Format(Messages.WeaponAlreadyExists, weaponSelectedFromDialog);
            player.SendClientMessage(Color.Red, message);
            await ShowWeapons(player);
            return;
        }

        selectedWeapons.Add(weaponSelectedFromDialog);
        player.GiveWeapon(weaponSelectedFromDialog.Id, IWeapon.UnlimitedAmmo);
        {
            var message = Smart.Format(Messages.WeaponSuccessfullyAdded, weaponSelectedFromDialog);
            player.SendClientMessage(Color.Yellow, message);
        }
        await ShowWeapons(player);
    }

    [PlayerCommand("weaponpack"), Alias("pack")]
    [ChangeDriversAttribute(ChangeDriver.Combat, ChangeDriver.WeaponCatalog, ChangeDriver.GunGame, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.Ecs, ChangeDriver.Player, ChangeDriver.CommandSet)]
    public async Task ShowWeaponPackage(Player player)
    {
        if (gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponPackUnavailable);
            return;
        }

        var weaponSelection = player.GetComponent<WeaponSelectionComponent>();
        WeaponPack selectedWeapons = weaponSelection.SelectedWeapons;
        if (selectedWeapons.IsEmpty())
        {
            player.SendClientMessage(Color.Red, Messages.EmptyWeaponPackage);
            return;
        }

        var dialog = new ListDialog("Your Weapon Pack", "Remove", "Close");
        foreach (IWeapon weapon in selectedWeapons)
            dialog.Add(weapon.Name);

        ListDialogResponse response = await dialogService.ShowAsync(player, dialog);
        if (response.IsRightButtonOrDisconnected())
            return;

        if (gunGameMode.IsEnabled)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponPackUnavailable);
            return;
        }

        Result<IWeapon> weaponResult = weaponCatalog.GetByName(response.Item.Text);
        if (weaponResult.IsFailed)
        {
            player.SendClientMessage(Color.Red, Messages.WeaponNoLongerAvailable);
            return;
        }

        IWeapon weaponSelectedFromDialog = weaponResult.Value;
        var message = Smart.Format(Messages.WeaponSuccessfullyRemoved, weaponSelectedFromDialog);
        player.SendClientMessage(Color.Red, message);
        selectedWeapons.Remove(weaponSelectedFromDialog);
        player.RemoveWeapon(weaponSelectedFromDialog.Id);
        await ShowWeaponPackage(player);
    }
}
