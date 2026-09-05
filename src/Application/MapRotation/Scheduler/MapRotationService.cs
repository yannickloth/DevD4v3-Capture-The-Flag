namespace CTF.Application.MapRotation.Scheduler;

/// <remarks>Injected dependencies (change drivers of these elements): serverService -> CD-42; mapObjects -> CD-37; worldService -> CD-36; timerService -> CD-41; mapInfoService -> CD-11; mapCollection -> CD-11; mapTextDrawRenderer -> CD-34; flagStateResetter -> CD-02; teamBalancer -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.Timer, ChangeDriver.ServerService)]
public class MapRotationService(
    IServerService serverService,
    IMapObjectService mapObjects,
    IWorldService worldService,
    ITimerService timerService,
    MapInfoService mapInfoService,
    MapCollection mapCollection,
    MapTextDrawRenderer mapTextDrawRenderer,
    FlagStateResetter flagStateResetter,
    TeamBalancer teamBalancer)
{
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private LoadTime _loadTime;

    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Timer)]
    private TimerReference _timerReference;

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private bool _isMapLoading;

    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map)]
    private IMap _forcedNextMap;

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private readonly TimeLeft _timeLeft = new();
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public TimeLeft TimeLeft => _timeLeft;
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public bool IsMapLoading => _isMapLoading;
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map)]
    public IMap NextMap => _forcedNextMap ?? mapCollection.GetNext(mapInfoService.CurrentMap);

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public delegate void LoadingMapEventHandler();
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public delegate void LoadedMapEventHandler();
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public event LoadingMapEventHandler LoadingMapEvent;
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules)</remarks>
    public event LoadedMapEventHandler LoadedMapEvent;

    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map)]
    public void ForceNextMap(IMap map)
    {
        ArgumentNullException.ThrowIfNull(map);
        _forcedNextMap = map;
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public void StartRotationTimer()
    {
        _loadTime ??= new LoadTime(OnLoadingMap, OnLoadedMap);
        _timerReference ??= timerService.Start(action: OnTimer, interval: TimeSpan.FromMilliseconds(1000));
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    public void StopRotationTimer()
    {
        if (_timerReference is null)
            return;

        timerService.Stop(_timerReference);
        _timerReference = default;
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.TextDraw)]
    private void OnTimer(IServiceProvider serviceProvider)
    {
        if (_timeLeft.IsCompleted())
        {
            _loadTime.Decrease();
            mapTextDrawRenderer.UpdateLoadTime(_loadTime);
            return;
        }

        _timeLeft.Decrease();
        mapTextDrawRenderer.UpdateTimeLeft(_timeLeft);
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.ClientMessage, ChangeDriver.ServerService)]
    private void OnLoadingMap()
    {
        _isMapLoading = true;
        LoadingMapEvent?.Invoke();
        mapObjects.Unload();

        IEnumerable<Player> players = MatchPlayers.GetAll();
        foreach (Player player in players)
            player.ToggleSpectating(true);

        IMap nextMap = NextMap;
        string message = Smart.Format(Messages.NextMapWillBeLoadedSoon, new { nextMap.Name });
        worldService.SendClientMessage(Color.Orange, message);
        mapInfoService.Load(nextMap);
        flagStateResetter.Reset(Team.Alpha, Team.Beta);
        mapObjects.Load(nextMap.Name);
        serverService.SetMapName(nextMap.Name);
    }

    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Map, ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.ServerService)]
    private void OnLoadedMap()
    {
        _isMapLoading = false;
        _forcedNextMap = default;
        LoadedMapEvent?.Invoke();
        TimeLeft.Reset();
        CurrentMap currentMap = mapInfoService.CurrentMap;
        string message = Smart.Format(Messages.MapSuccessfullyLoaded, new { currentMap.Name });
        worldService.SendClientMessage(Color.Orange, message);
        static void PreparePlayerForRound(Player player, PlayerInfo playerInfo)
        {
            playerInfo.Stats.PerRound.ResetStats();
            player.ToggleControllable(true);
            player.Health = 100;
            player.Color = playerInfo.Appearance.Team.ColorHex;
            player.SetScore(0);
            player.ToggleSpectating(false);
        }
        teamBalancer.Balance(Team.Alpha, Team.Beta, onPlayerAssigned: PreparePlayerForRound);
        worldService.SetWeather(currentMap.Weather);
        serverService.SetWorldTime(currentMap.WorldTime);
        mapTextDrawRenderer.UpdateMapName(currentMap);
    }
}
