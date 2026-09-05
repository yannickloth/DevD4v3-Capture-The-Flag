namespace CTF.Application.MapIcons.TeamIcons;

/// <summary>
/// Creates and destroys dynamic map icons for team flags.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.MapIcon, ChangeDriver.Map, ChangeDriver.Pickup)]
public class TeamIconService
{
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    private readonly MapInfoService _mapInfoService;

    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    private readonly IStreamerService _streamerService;

    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    private DynamicMapIcon _redMapIcon;

    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    private DynamicMapIcon _blueMapIcon;

    /// <remarks>Change drivers: CD-38 (root; map-icon API)</remarks>
    public TeamIconService(MapInfoService mapInfoService, IStreamerService streamerService)
    {
        _mapInfoService = mapInfoService;
        _streamerService = streamerService;
        CreateFromBasePosition(Team.Alpha);
        CreateFromBasePosition(Team.Beta);
    }

    /// <summary>Creates the map icon from the team's base position.</summary>
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
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
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
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
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
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
    [ChangeDriversAttribute(ChangeDriver.MapIcon)]
    public void DestroyAll()
    {
        Destroy(Team.Alpha);
        Destroy(Team.Beta);
    }
}
