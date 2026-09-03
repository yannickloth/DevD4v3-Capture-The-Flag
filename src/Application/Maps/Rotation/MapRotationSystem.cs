namespace CTF.Application.Maps.Rotation;

/// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12; CD-01 (open.mp/SampSharp platform API) → CD-12; CD-15 (command set) → CD-12; CD-09 (authorization policy) → CD-12</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; dialogService -> CD-01; mapRotationService -> CD-12; mapCollection -> CD-11; mapTextDrawRenderer -> CD-01. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class MapRotationSystem(
    IWorldService worldService,
    IDialogService dialogService,
    MapRotationService mapRotationService,
    MapCollection mapCollection,
    MapTextDrawRenderer mapTextDrawRenderer) : ISystem
{
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules: connected-player count)</remarks>
    private int _connectedPlayers;

    [Event]
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
    public void OnPlayerSpawn(Player player)
    {
        mapTextDrawRenderer.Show(player);
    }

    [Event]
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
    public void OnPlayerConnect(Player player)
    {
        _connectedPlayers++;
        if (_connectedPlayers == 1)
            mapRotationService.StartRotationTimer();
    }

    [Event]
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        _connectedPlayers--;
        if (_connectedPlayers == 0)
            mapRotationService.StopRotationTimer();
    }

    [PlayerCommand("startrt")]
    [RequiresMinimumRole(RoleId.Moderator)]
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12; CD-15 (command set) → CD-12; CD-09 (authorization policy) → CD-12</remarks>
    public void StartRotationTimer(Player player)
    {
        mapRotationService.StartRotationTimer();
    }

    [PlayerCommand("stoprt")]
    [RequiresMinimumRole(RoleId.Moderator)]
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12; CD-15 (command set) → CD-12; CD-09 (authorization policy) → CD-12</remarks>
    public void StopRotationTimer(Player player)
    {
        mapRotationService.StopRotationTimer();
    }

    [PlayerCommand("settimeleft")]
    [RequiresMinimumRole(RoleId.Moderator)]
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12; CD-15 (command set) → CD-12; CD-09 (authorization policy) → CD-12</remarks>
    public void SetTimeLeft(Player player, int minutes)
    {
        var interval = new Minutes(minutes);
        TimeLeft timeLeft = mapRotationService.TimeLeft;
        Result result = timeLeft.SetInterval(interval);
        if (result.IsFailed)
        {
            player.SendClientMessage(Color.Red, result.Message);
            return;
        }

        mapTextDrawRenderer.UpdateTimeLeft(timeLeft);
    }

    [PlayerCommand("maps")]
    [RequiresMinimumRole(RoleId.Moderator)]
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12; CD-01 (open.mp/SampSharp platform API) → CD-12; CD-15 (command set) → CD-12; CD-09 (authorization policy) → CD-12</remarks>
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

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12; CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
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
