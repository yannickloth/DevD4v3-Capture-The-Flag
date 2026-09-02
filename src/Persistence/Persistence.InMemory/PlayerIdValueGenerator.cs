namespace Persistence.InMemory;

/// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-21 (DI container/composition) → CD-18</remarks>
internal class PlayerIdValueGenerator
{
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-21 (DI container/composition) → CD-18</remarks>
    private PlayerIdValueGenerator() { }
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model)</remarks>
    private int _current = 1;
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-21 (DI container/composition) → CD-18</remarks>
    public static PlayerIdValueGenerator Instance { get; } = new();
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model)</remarks>
    public int Next() => _current++;
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model); CD-21 (DI container/composition) → CD-18</remarks>
    public int Reset() => _current = 1;
}
