namespace CTF.Application.PlayerResources.Appearance;

/// <summary>
/// Represents the persisted visual/platform preferences of a player.
/// </summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    public class PlayerAppearance
{
    /// <summary>
    /// It is a sentinel value that indicates the player has no skin selected.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Model)]
    private const int NoSkin = -1;

    [ChangeDriversAttribute(ChangeDriver.Model)]
    public int SkinId { get; private set; } = NoSkin;

    [ChangeDriversAttribute(ChangeDriver.Model)]
    public Team Team { get; private set; } = Team.None;

    [ChangeDriversAttribute(ChangeDriver.Model)]
    public void RemoveSkin() => SkinId = NoSkin;

    [ChangeDriversAttribute(ChangeDriver.Model)]
    public Result SetSkin(int id)
    {
        if (id < 0 || id > 311)
            return Result.Failure(Messages.InvalidSkin);

        SkinId = id;
        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Model)]
    public Result SetTeam(TeamId id)
    {
        Result<Team> result = id switch
        {
            TeamId.Alpha  => Result<Team>.Success(Team.Alpha),
            TeamId.Beta   => Result<Team>.Success(Team.Beta),
            TeamId.NoTeam => Result<Team>.Success(Team.None),
            _ => Result<Team>.Failure()
        };

        if (result.IsSuccess)
        {
            Team = result.Value;
            return Result.Success();
        }

        return Result.Failure(Messages.InvalidTeam);
    }
}
