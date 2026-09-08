namespace CTF.Application.Maps;

/// <summary>Test double for the IMap interface.</summary>
[ChangeDriversAttribute(ChangeDriver.Map)]
public class FakeMap(
    int id = 0,
    string name = "RC_Battlefield") : IMap
{
    public int Id { get; } = id;

    public string Name { get; } = name;
}
