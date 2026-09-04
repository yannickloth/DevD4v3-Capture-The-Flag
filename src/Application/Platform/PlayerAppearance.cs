namespace CTF.Application.Platform;

/// <summary>
/// Represents the persisted visual/platform preferences of a player.
/// </summary>
/// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
public class PlayerAppearance
{
    /// <summary>
    /// It is a sentinel value that indicates the player has no skin selected.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: skin id, no-skin sentinel)</remarks>
    private const int NoSkin = -1;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API); CD-20 (outbound repository contract) → CD-01</remarks>
    public int SkinId { get; private set; } = NoSkin;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public Team Team { get; private set; } = Team.None;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public void RemoveSkin() => SkinId = NoSkin;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public Result SetSkin(int id)
    {
        if (id < 0 || id > 311)
            return Result.Failure(Messages.InvalidSkin);

        SkinId = id;
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
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
