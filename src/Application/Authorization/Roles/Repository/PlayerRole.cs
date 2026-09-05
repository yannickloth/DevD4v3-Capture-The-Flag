namespace CTF.Application.Authorization.Roles.Repository;

/// <summary>
/// Represents the persisted authorization state of a player.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository)]
public class PlayerRole
{
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository)]
    public RoleId Id { get; private set; } = RoleId.Basic;

    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository)]
    public Result Set(RoleId id)
    {
        if (id < 0 || (int)id >= RoleCollection.Count)
            return Result.Failure(Messages.InvalidRole);

        Id = id;
        return Result.Success();
    }
}
