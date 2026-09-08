namespace CTF.Application.Maps;

[ChangeDriversAttribute(ChangeDriver.Map)]
public class SpawnLocation
{
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public static readonly SpawnLocation Empty = new(0, 0, 0, 0);
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public Vector3 Position { get; }
    [ChangeDriversAttribute(ChangeDriver.Map)]
    public float Angle { get; }
    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public SpawnLocation(float x, float y, float z, float angle)
    {
        Position = new Vector3(x, y, z);
        Angle = angle;
    }

    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public SpawnLocation(Vector3 position, float angle)
    {
        Position = position;
        Angle = angle;
    }
}
