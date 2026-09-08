namespace CTF.Application.SchemaNs2;

[ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
internal class PlayerIdValueGenerator
{
    /// <remarks>Change drivers: CD-18 (root; database schema/player data model)</remarks>
    private PlayerIdValueGenerator() { }
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
    private int _current = 1;
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
    public static PlayerIdValueGenerator Instance { get; } = new();
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
    public int Next() => _current++;
    [ChangeDriversAttribute(ChangeDriver.DatabaseSchema)]
    public int Reset() => _current = 1;
}
