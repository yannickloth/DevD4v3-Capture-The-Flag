namespace CTF.Application.Platform;

/// <summary>
/// Creates and destroys pickups for team flags and exterior markers.
/// </summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups/model ids); CD-11 (map configuration: flag locations) → CD-01</remarks>
public class TeamPickupService
{
    /// <remarks>Change drivers: CD-11 (root; map configuration: flag locations)</remarks>
    private readonly MapInfoService _mapInfoService;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: injected world service)</remarks>
    private readonly IWorldService _worldService;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickup); CD-11 (map configuration: flag locations) → CD-01</remarks>
    private Pickup _redFlagPickup;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickup); CD-11 (map configuration: flag locations) → CD-01</remarks>
    private Pickup _blueFlagPickup;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickup); CD-11 (map configuration: flag locations) → CD-01</remarks>
    private Pickup _redExteriorMarker;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickup); CD-11 (map configuration: flag locations) → CD-01</remarks>
    private Pickup _blueExteriorMarker;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups); CD-11 (map configuration: flag locations) → CD-01</remarks>
    public TeamPickupService(MapInfoService mapInfoService, IWorldService worldService)
    {
        _mapInfoService = mapInfoService;
        _worldService = worldService;
        CreateFlagFromBasePosition(Team.Alpha);
        CreateFlagFromBasePosition(Team.Beta);
    }

    /// <summary>Creates the flag pickup at the team's base position.</summary>
    /// <remarks>Change drivers: CD-11 (root; map configuration: flag locations)</remarks>
    public void CreateFlagFromBasePosition(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);
        CurrentMap currentMap = _mapInfoService.CurrentMap;
        if (team.Id == TeamId.Alpha)
        {
            CreateFlagFromVector3(team, currentMap.FlagLocations.Red);
        }
        else if (team.Id == TeamId.Beta)
        {
            CreateFlagFromVector3(team, currentMap.FlagLocations.Blue);
        }
    }

    /// <summary>Creates the flag pickup at the specified position.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups); CD-11 (map configuration: flag location) → CD-01</remarks>
    public void CreateFlagFromVector3(Team team, Vector3 position)
    {
        ArgumentNullException.ThrowIfNull(team);
        DestroyFlag(team);
        if (team.Id == TeamId.Alpha)
        {
            _redFlagPickup = _worldService.CreatePickup(
                model: (int)Team.Alpha.Flag.Model,
                type: PickupType.ScriptedActionsOnlyEveryFewSeconds,
                position
            );
        }
        else if (team.Id == TeamId.Beta)
        {
            _blueFlagPickup = _worldService.CreatePickup(
               model: (int)Team.Beta.Flag.Model,
               type: PickupType.ScriptedActionsOnlyEveryFewSeconds,
               position
            );
        }
    }

    /// <summary>Destroys the flag pickup for the specified team.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups)</remarks>
    public void DestroyFlag(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);
        if (team.Id == TeamId.Alpha)
        {
            _redFlagPickup?.Destroy();
            _redFlagPickup = default;
        }
        else if (team.Id == TeamId.Beta)
        {
            _blueFlagPickup?.Destroy();
            _blueFlagPickup = default;
        }
    }

    /// <summary>Destroys all flag pickups.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups); CD-11 (map configuration: flag locations) → CD-01</remarks>
    public void DestroyFlags()
    {
        DestroyFlag(Team.Alpha);
        DestroyFlag(Team.Beta);
    }

    /// <summary>Creates the exterior marker for the specified team.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups); CD-11 (map configuration: flag locations) → CD-01</remarks>
    public void CreateExteriorMarker(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);
        CurrentMap currentMap = _mapInfoService.CurrentMap;
        DestroyExteriorMarker(team);
        if (team.Id == TeamId.Alpha)
        {
            _redExteriorMarker = _worldService.CreatePickup(
                model: (int)ExteriorMarker.Red,
                type: PickupType.ScriptedActionsOnlyEveryFewSeconds,
                position: currentMap.FlagLocations.Red
            );
        }
        else if (team.Id == TeamId.Beta)
        {
            _blueExteriorMarker = _worldService.CreatePickup(
                model: (int)ExteriorMarker.Blue,
                type: PickupType.ScriptedActionsOnlyEveryFewSeconds,
                position: currentMap.FlagLocations.Blue
            );
        }
    }

    /// <summary>Destroys the exterior marker for the specified team.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups)</remarks>
    public void DestroyExteriorMarker(Team team)
    {
        ArgumentNullException.ThrowIfNull(team);
        if (team.Id == TeamId.Alpha)
        {
            _redExteriorMarker?.Destroy();
            _redExteriorMarker = default;
        }
        else if (team.Id == TeamId.Beta)
        {
            _blueExteriorMarker?.Destroy();
            _blueExteriorMarker = default;
        }
    }

    /// <summary>Destroys all pickups for flags and exterior markers.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: pickups)</remarks>
    public void DestroyAllPickups()
    {
        DestroyExteriorMarker(Team.Alpha);
        DestroyExteriorMarker(Team.Beta);
        DestroyFlags();
    }
}
