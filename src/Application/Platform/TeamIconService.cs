namespace CTF.Application.Platform;

/// <summary>
/// Creates and destroys dynamic map icons for team flags.
/// </summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map icons); CD-11 (map configuration: flag locations/interior) → CD-01</remarks>
public class TeamIconService
{
    /// <remarks>Change drivers: CD-11 (root; map configuration: flag locations); CD-01 (open.mp/SampSharp platform API: map icon dependencies) → CD-11</remarks>
    private readonly MapInfoService _mapInfoService;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: injected streamer service)</remarks>
    private readonly IStreamerService _streamerService;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map icon); CD-11 (map configuration: flag location/interior) → CD-01</remarks>
    private DynamicMapIcon _redMapIcon;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map icon); CD-11 (map configuration: flag location/interior) → CD-01</remarks>
    private DynamicMapIcon _blueMapIcon;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map icons); CD-11 (map configuration: flag locations) → CD-01</remarks>
    public TeamIconService(MapInfoService mapInfoService, IStreamerService streamerService)
    {
        _mapInfoService = mapInfoService;
        _streamerService = streamerService;
        CreateFromBasePosition(Team.Alpha);
        CreateFromBasePosition(Team.Beta);
    }

    /// <summary>Creates the map icon from the team's base position.</summary>
    /// <remarks>Change drivers: CD-11 (root; map configuration: flag locations)</remarks>
    public void CreateFromBasePosition(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);
        CurrentMap currentMap = _mapInfoService.CurrentMap;
        if (team.Id == TeamId.Alpha)
        {
            CreateFromVector3(team, currentMap.FlagLocations.Red);
        }
        else if (team.Id == TeamId.Beta)
        {
            CreateFromVector3(team, currentMap.FlagLocations.Blue);
        }
    }

    /// <summary>Creates the map icon at the specified position.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map icons); CD-11 (map configuration: flag location/interior) → CD-01</remarks>
    public void CreateFromVector3(Team team, Vector3 position)
    {
        ArgumentNullException.ThrowIfNull(team);
        CurrentMap currentMap = _mapInfoService.CurrentMap;
        Destroy(team);
        if (team.Id == TeamId.Alpha)
        {
            _redMapIcon = _streamerService.CreateDynamicMapIcon(
                position: position,
                type: (MapIcon)Team.Alpha.Flag.Icon,
                streamDistance: 5000f,
                interior: currentMap.Interior,
                color: Team.Alpha.Flag.ColorHex
            );
        }
        else if (team.Id == TeamId.Beta)
        {
            _blueMapIcon = _streamerService.CreateDynamicMapIcon(
                position: position,
                type: (MapIcon)Team.Beta.Flag.Icon,
                streamDistance: 5000f,
                interior: currentMap.Interior,
                color: Team.Beta.Flag.ColorHex
            );
        }
    }

    /// <summary>Destroys the map icon for the specified team.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map icons)</remarks>
    public void Destroy(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);
        if (team.Id == TeamId.Alpha)
        {
            _redMapIcon?.Destroy();
            _redMapIcon = default;
        }
        else if (team.Id == TeamId.Beta)
        {
            _blueMapIcon?.Destroy();
            _blueMapIcon = default;
        }
    }

    /// <summary>Destroys all team map icons.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: map icons)</remarks>
    public void DestroyAll()
    {
        Destroy(Team.Alpha);
        Destroy(Team.Beta);
    }
}
