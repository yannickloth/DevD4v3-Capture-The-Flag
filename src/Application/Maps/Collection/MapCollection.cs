namespace CTF.Application.Maps.Collection;

[ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.MapRotation)]
public class MapCollection
{
    [ChangeDriversAttribute(ChangeDriver.Map)]
    private Map[] _maps;

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public MapCollection(string mapsPath)
    {
        LoadFromDirectory(mapsPath);
    }

    [ChangeDriversAttribute(ChangeDriver.Map)]
    public int Count => _maps.Length;
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public IReadOnlyList<IMap> GetAll() => _maps;
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public IEnumerable<IMap> GetAll(string findBy)
    {
        foreach (Map map in _maps)
        {
            if (map.Name.StartsWith(findBy, StringComparison.OrdinalIgnoreCase))
                yield return map;
        }
    }

    [ChangeDriversAttribute(ChangeDriver.Map)]
    public Result<IMap> GetById(int id)
    {
        if (id < 0 || id >= Count)
            return Result<IMap>.Failure(Messages.InvalidMap);

        Map map = _maps[id];
        return Result<IMap>.Success(map);
    }

    [ChangeDriversAttribute(ChangeDriver.Map)]
    public Result<IMap> GetByName(string mapName)
    {
        Map map = _maps
            .FirstOrDefault(map => map.Name.Equals(mapName, StringComparison.OrdinalIgnoreCase));
        return map is null ?
            Result<IMap>.Failure(Messages.MapNotFound) :
            Result<IMap>.Success(map);
    }

    [ChangeDriversAttribute(ChangeDriver.Map, ChangeDriver.MapRotation)]
    public IMap GetNext(IMap current)
    {
        int nextMapId = (current.Id + 1) % Count;
        return GetById(nextMapId).Value;
    }

    [ChangeDriversAttribute(ChangeDriver.Map)]
    private class Map : IMap
    {
        [ChangeDriversAttribute(ChangeDriver.Map)]
        public int Id { get; init; }
        [ChangeDriversAttribute(ChangeDriver.Map)]
        public string Name { get; init; }
    }

    [ChangeDriversAttribute(ChangeDriver.Map)]
    private void LoadFromDirectory(string mapsPath)
    {
        var random = new Random();
        string[] names = Directory.GetFiles(mapsPath);
        random.Shuffle(names);
        _maps = new Map[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            var map = new Map
            {
                Id = i,
                Name = Path.GetFileNameWithoutExtension(names[i])
            };
            _maps[i] = map;
        }
    }
}
