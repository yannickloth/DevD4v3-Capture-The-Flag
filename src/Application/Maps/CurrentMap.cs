namespace CTF.Application.Maps;

/// <summary>
/// Represents the current information of a map.
/// </summary>
/// <remarks>Change drivers: CD-11 (map configuration)</remarks>
public class CurrentMap : IMap
{
    private readonly Random _random = new();
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public const int DefaultInterior  = 0;
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public const int DefaultWeather   = 10;
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public const int DefaultWorldTime = 12;
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public int Id { get; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public string Name { get; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public IReadOnlyList<SpawnLocation> AlphaTeamLocations { get; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public IReadOnlyList<SpawnLocation> BetaTeamLocations { get; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public FlagLocations FlagLocations { get; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public int Interior { get; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public int Weather { get; }
    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public int WorldTime { get; }

    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public CurrentMap(
        IMap map, 
        IReadOnlyList<SpawnLocation> alphaTeamLocations, 
        IReadOnlyList<SpawnLocation> betaTeamLocations,
        FlagLocations flagLocations,
        int interior  = DefaultInterior,
        int weather   = DefaultWeather,
        int worldTime = DefaultWorldTime)
    {
        ArgumentNullException.ThrowIfNull(map);
        ArgumentNullException.ThrowIfNull(alphaTeamLocations);
        ArgumentNullException.ThrowIfNull(betaTeamLocations);
        ArgumentNullException.ThrowIfNull(flagLocations);

        if (alphaTeamLocations.Count == 0)
            throw new ArgumentException(Messages.LocationListCannotBeEmpty, nameof(alphaTeamLocations));

        if (betaTeamLocations.Count == 0)
            throw new ArgumentException(Messages.LocationListCannotBeEmpty, nameof(betaTeamLocations));

        Id = map.Id;
        Name = map.Name;
        AlphaTeamLocations = alphaTeamLocations;
        BetaTeamLocations = betaTeamLocations;
        FlagLocations = flagLocations;
        Interior = interior;
        Weather = weather;
        WorldTime = worldTime;
    }

    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public string GetMapNameAsText() 
        => $"Map: ~w~{Name}";

    /// <remarks>Change drivers: CD-11 (map configuration)</remarks>
    public SpawnLocation GetRandomSpawnLocation(TeamId team) => team switch
    {
        TeamId.Alpha => AlphaTeamLocations[_random.Next(AlphaTeamLocations.Count)],
        TeamId.Beta => BetaTeamLocations[_random.Next(BetaTeamLocations.Count)],
        _ => throw new NotSupportedException(Messages.SpawnLocationFailure)
    };
}
