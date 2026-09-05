namespace CTF.Application.Maps;

[ChangeDriversAttribute(ChangeDriver.Map)]
public class FlagLocations
{
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public static readonly FlagLocations Empty = new()
    {
        Red  = new Vector3(0f, 0f, 0f),
        Blue = new Vector3(0f, 0f, 0f)
    };

    [ChangeDriversAttribute(ChangeDriver.Map)]
    public required Vector3 Red { get; init; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public required Vector3 Blue { get; init; }
}
