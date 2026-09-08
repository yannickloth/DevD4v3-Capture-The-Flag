namespace CTF.Application.Maps;

/// <summary>
/// Represents the current information of a map.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Map)]
public class CurrentMap : IMap
{
    [ChangeDriversAttribute(ChangeDriver.Map)]
    private readonly Random _random = new();

    [ChangeDriversAttribute(ChangeDriver.Map)]
    public const int DefaultInterior  = 0;
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public const int DefaultWeather   = 10;
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public const int DefaultWorldTime = 12;
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public int Id { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public string Name { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public IReadOnlyList<SpawnLocation> AlphaTeamLocations { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public IReadOnlyList<SpawnLocation> BetaTeamLocations { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public FlagLocations FlagLocations { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public int Interior { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public int Weather { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public int WorldTime { get; }

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
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

    [ChangeDriversAttribute(ChangeDriver.Map)]
    public string GetMapNameAsText() 
        => $"Map: ~w~{Name}";

    [ChangeDriversAttribute(ChangeDriver.Map)]
    public SpawnLocation GetRandomSpawnLocation(TeamId team) => team switch
    {
        TeamId.Alpha => AlphaTeamLocations[_random.Next(AlphaTeamLocations.Count)],
        TeamId.Beta => BetaTeamLocations[_random.Next(BetaTeamLocations.Count)],
        _ => throw new NotSupportedException(Messages.SpawnLocationFailure)
    };
}
