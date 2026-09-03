namespace CTF.Application.Authorization;

/// <summary>
/// Represents the persisted authorization state of a player.
/// </summary>
/// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
public class PlayerRole
{
    /// <remarks>Change drivers: CD-09 (root; authorization policy); CD-20 (outbound repository contract) → CD-09</remarks>
    public RoleId Id { get; private set; } = RoleId.Basic;

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public Result Set(RoleId id)
    {
        if (id < 0 || (int)id >= RoleCollection.Count)
            return Result.Failure(Messages.InvalidRole);

        Id = id;
        return Result.Success();
    }
}
