namespace CTF.Application.MapRotation.Commands.MapSelection;

/// <summary>
/// Provides the moderator command to browse and force-select the next map.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; dialogService -> CD-33; mapRotationService -> CD-12; mapCollection -> CD-11; mapTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
public class MapSelectionCommands(
    IWorldService worldService,
    IDialogService dialogService,
    MapRotationService mapRotationService,
    MapCollection mapCollection,
    MapTextDrawRenderer mapTextDrawRenderer) : ISystem
{
    [PlayerCommand("maps")]
    [RequiresMinimumRole(RoleId.Moderator)]
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
    public async Task ShowMaps(Player player, string findBy = default)
    {
        var listDialog = new ListDialog(string.Empty, "Select", "Close");
        IEnumerable<IMap> maps = string.IsNullOrEmpty(findBy) ?
            mapCollection.GetAll() :
            mapCollection.GetAll(findBy);

        IMap nextMap = mapRotationService.NextMap;
        foreach (IMap map in maps)
        {
            if (map.Id == nextMap.Id)
                listDialog.Add(text: $"{map.Name} {Color.Red}[Next Map]", tag: map.Id);
            else
                listDialog.Add(text: map.Name, tag: map.Id);
        }

        if (listDialog.Rows.Count == 0)
        {
            player.SendClientMessage(Color.Red, Messages.NoMatchFound);
            return;
        }

        listDialog.Caption = $"Maps: {listDialog.Rows.Count}/{mapCollection.Count}";
        ListDialogResponse listDialogResponse = await dialogService.ShowAsync(player, listDialog);
        if (listDialogResponse.Response == DialogResponse.LeftButton)
        {
            int selectedMapId = (int)listDialogResponse.Item.Tag;
            IMap selectedMap = mapCollection.GetById(selectedMapId).Value;
            await ShowConfirmationDialog(player, selectedMap);
        }
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map, ChangeDriver.CommandInfrastructure, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
    private async Task ShowConfirmationDialog(Player player, IMap selectedMap)
    {
        var confirmationDialog = new MessageDialog(
            caption: "Confirmation",
            content: "Do you want to force the map change right now?",
            button1: "Yes",
            button2: "No"
        );
        MessageDialogResponse confirmationDialogResponse = await dialogService.ShowAsync(player, confirmationDialog);
        if (confirmationDialogResponse.Response == DialogResponse.Disconnected)
            return;

        if (mapRotationService.IsMapLoading)
        {
            player.SendClientMessage(Color.Red, Messages.MapIsLoading);
            return;
        }

        if (confirmationDialogResponse.Response == DialogResponse.LeftButton)
        {
            TimeLeft timeLeft = mapRotationService.TimeLeft;
            timeLeft.SetInterval(new Minutes(0));
            mapTextDrawRenderer.UpdateTimeLeft(timeLeft);
            var message = Smart.Format(Messages.MapChangeForced, new
            {
                PlayerName = player.Name,
                MapName = selectedMap.Name
            });
            worldService.SendClientMessage(Color.Orange, message);
            mapRotationService.ForceNextMap(selectedMap);
        }
        else if (confirmationDialogResponse.Response == DialogResponse.RightButtonOrCancel)
        {
            var message = Smart.Format(Messages.NextMapSelection, new
            {
                PlayerName = player.Name,
                MapName = selectedMap.Name
            });
            worldService.SendClientMessage(Color.Orange, message);
            mapRotationService.ForceNextMap(selectedMap);
        }
    }
}
