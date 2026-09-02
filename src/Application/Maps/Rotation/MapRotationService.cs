namespace CTF.Application.Maps.Rotation;

/// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12; CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): serverService -> CD-01; mapObjects -> CD-01; worldService -> CD-01; timerService -> CD-01; mapInfoService -> CD-29+CD-11; mapCollection -> CD-29+CD-11; mapTextDrawRenderer -> CD-29+CD-01; flagStateResetter -> CD-29+CD-02; teamBalancer -> CD-29+CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
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
    private LoadTime _loadTime;
    private TimerReference _timerReference;
    private bool _isMapLoading;
    private IMap _forcedNextMap;
    private readonly TimeLeft _timeLeft = new();
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public TimeLeft TimeLeft => _timeLeft;
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public bool IsMapLoading => _isMapLoading;
    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12</remarks>
    public IMap NextMap => _forcedNextMap ?? mapCollection.GetNext(mapInfoService.CurrentMap);

    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public delegate void LoadingMapEventHandler();
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public delegate void LoadedMapEventHandler();
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public event LoadingMapEventHandler LoadingMapEvent;
    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public event LoadedMapEventHandler LoadedMapEvent;

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12</remarks>
    public void ForceNextMap(IMap map)
    {
        ArgumentNullException.ThrowIfNull(map);
        _forcedNextMap = map;
    }

    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public void StartRotationTimer()
    {
        _loadTime ??= new LoadTime(OnLoadingMap, OnLoadedMap);
        _timerReference ??= timerService.Start(action: OnTimer, interval: TimeSpan.FromMilliseconds(1000));
    }

    /// <remarks>Change drivers: CD-12 (root; root; map-rotation rules)</remarks>
    public void StopRotationTimer()
    {
        if (_timerReference is null)
            return;

        timerService.Stop(_timerReference);
        _timerReference = default;
    }

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
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

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12; CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
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

    /// <remarks>Change drivers: CD-12 (root; map-rotation rules); CD-11 (map configuration) → CD-12; CD-01 (open.mp/SampSharp platform API) → CD-12</remarks>
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
            playerInfo.StatsPerRound.ResetStats();
            player.ToggleControllable(true);
            player.Health = 100;
            player.Color = playerInfo.Team.ColorHex;
            player.SetScore(0);
            player.ToggleSpectating(false);
        }
        teamBalancer.Balance(Team.Alpha, Team.Beta, onPlayerAssigned: PreparePlayerForRound);
        worldService.SetWeather(currentMap.Weather);
        serverService.SetWorldTime(currentMap.WorldTime);
        mapTextDrawRenderer.UpdateMapName(currentMap);
    }
}
