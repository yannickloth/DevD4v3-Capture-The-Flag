namespace CTF.Application.Maps.ServerServiceDomain;

/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; serverService -> CD-42; mapObjects -> CD-37; mapInfoService -> CD-11; mapCollection -> CD-11; teamPickupService -> CD-37; teamIconService -> CD-38; mapTextDrawRenderer -> CD-34; serverSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.Configuration, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.ServerService)]
public class MapInitializationSystem(
    IWorldService worldService,
    IServerService serverService,
    IMapObjectService mapObjects,
    MapInfoService mapInfoService,
    MapCollection mapCollection,
    TeamPickupService teamPickupService,
    TeamIconService teamIconService,
    MapTextDrawRenderer mapTextDrawRenderer,
    ServerSettings serverSettings) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.Configuration, ChangeDriver.Ecs, ChangeDriver.TextDraw, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.ServerService)]
    public void OnGameModeInit()
    {
        Result<IMap> mapResult = mapCollection.GetByName(serverSettings.MapName);
        if (mapResult.IsSuccess)
        {
            mapInfoService.Load(mapResult.Value);
        }

        CurrentMap currentMap = mapInfoService.CurrentMap;
        serverService.SetMapName(currentMap.Name);
        mapObjects.Load(currentMap.Name);
        mapTextDrawRenderer.UpdateMapName(currentMap);

        worldService.SetWeather(currentMap.Weather);
        serverService.SetWorldTime(currentMap.WorldTime);
        teamPickupService.CreateFlagFromBasePosition(Team.Alpha);
        teamPickupService.CreateFlagFromBasePosition(Team.Beta);
        teamIconService.CreateFromBasePosition(Team.Alpha);
        teamIconService.CreateFromBasePosition(Team.Beta);
    }
}
