namespace CTF.Application.Maps;

/// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
public class SpawnLocation
{
    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public static readonly SpawnLocation Empty = new(0, 0, 0, 0);
    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
    public Vector3 Position { get; }
    /// <remarks>Change drivers: CD-11 (root; map configuration)</remarks>
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
