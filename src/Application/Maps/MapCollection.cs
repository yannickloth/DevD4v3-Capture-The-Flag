namespace CTF.Application.Maps;

/// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
public class MapCollection
{
    private Map[] _maps;

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public MapCollection(string mapsPath)
    {
        LoadFromDirectory(mapsPath);
    }

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public int Count => _maps.Length;
    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public IReadOnlyList<IMap> GetAll() => _maps;
    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public IEnumerable<IMap> GetAll(string findBy)
    {
        foreach (Map map in _maps)
        {
            if (map.Name.StartsWith(findBy, StringComparison.OrdinalIgnoreCase))
                yield return map;
        }
    }

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public Result<IMap> GetById(int id)
    {
        if (id < 0 || id >= Count)
            return Result<IMap>.Failure(Messages.InvalidMap);

        Map map = _maps[id];
        return Result<IMap>.Success(map);
    }

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public Result<IMap> GetByName(string mapName)
    {
        Map map = _maps
            .FirstOrDefault(map => map.Name.Equals(mapName, StringComparison.OrdinalIgnoreCase));
        return map is null ?
            Result<IMap>.Failure(Messages.MapNotFound) :
            Result<IMap>.Success(map);
    }

    /// <remarks>Change drivers: CD-11 (root; map configuration); CD-12 (map-rotation rules) → CD-11</remarks>
    public IMap GetNext(IMap current)
    {
        int nextMapId = (current.Id + 1) % Count;
        return GetById(nextMapId).Value;
    }

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    private class Map : IMap
    {
        /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
        public int Id { get; init; }
        /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
        public string Name { get; init; }
    }

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
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
