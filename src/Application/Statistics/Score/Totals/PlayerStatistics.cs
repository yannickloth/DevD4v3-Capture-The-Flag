namespace CTF.Application.Statistics.Score.Totals;

/// <summary>
/// Represents the persisted career statistics of a player.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
public class PlayerStatistics
{
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public PlayerStatsPerRound PerRound { get; } = new();

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int TotalKills { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int TotalDeaths { get; private set; }

    /// <summary>
    /// Indicates the maximum killing spree.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int MaxKillingSpree { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has captured the opposing team's flag and brought it back to their own base.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int BroughtFlags { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has captured the opposing team's flag from their base.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int CapturedFlags { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has dropped the opposing team's flag.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int DroppedFlags { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has returned the flag to their team's base.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int ReturnedFlags { get; private set; }

    /// <summary>
    /// Indicates the number of shots that the player has made at the heads of other players.
    /// </summary>
    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int HeadShots { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public int GunGameWins { get; private set; }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public RankId RankId { get; private set; } = RankId.Noob;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public DateTime LastConnection { get; private set; } = DateTime.UtcNow;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void SetLastConnection() => LastConnection = DateTime.UtcNow;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void SetMaxKillingSpree(int value) => MaxKillingSpree = value;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddTotalKills() => TotalKills++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddTotalDeaths() => TotalDeaths++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddBroughtFlags() => BroughtFlags++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddCapturedFlags() => CapturedFlags++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddDroppedFlags() => DroppedFlags++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddReturnedFlags() => ReturnedFlags++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddHeadShots() => HeadShots++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public void AddGunGameWins() => GunGameWins++;

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public Result SetTotalKills(int value)
    {
        if (value < 0)
            return Result.Failure(Messages.ValueCannotBeNegative);

        TotalKills = value;
        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public Result SetTotalDeaths(int value)
    {
        if (value < 0)
            return Result.Failure(Messages.ValueCannotBeNegative);

        TotalDeaths = value;
        return Result.Success();
    }

    [ChangeDriversAttribute(ChangeDriver.Statistics, ChangeDriver.Repository)]
    public Result SetRank(RankId id)
    {
        if (id < 0 || (int)id >= RankCollection.Count)
            return Result.Failure(Messages.InvalidRank);

        RankId = id;
        return Result.Success();
    }
}
