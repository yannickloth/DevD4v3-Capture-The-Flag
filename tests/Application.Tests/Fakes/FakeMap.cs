namespace CTF.Application.Tests.Fakes;

/// <summary>Test double for the IMap interface.</summary>
/// <remarks>Change drivers: CD-11 (root; map configuration: the IMap interface)</remarks>
public class FakeMap(
    int id = 0,
    string name = "RC_Battlefield") : IMap
{
    public int Id { get; } = id;

    public string Name { get; } = name;
}
