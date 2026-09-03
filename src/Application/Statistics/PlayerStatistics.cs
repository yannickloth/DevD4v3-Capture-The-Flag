namespace CTF.Application.Statistics;

/// <summary>
/// Represents the persisted career statistics of a player.
/// </summary>
/// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
public class PlayerStatistics
{
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public PlayerStatsPerRound PerRound { get; } = new();

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int TotalKills { get; private set; }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int TotalDeaths { get; private set; }

    /// <summary>
    /// Indicates the maximum killing spree.
    /// </summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int MaxKillingSpree { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has captured the opposing team's flag and brought it back to their own base.
    /// </summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int BroughtFlags { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has captured the opposing team's flag from their base.
    /// </summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int CapturedFlags { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has dropped the opposing team's flag.
    /// </summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int DroppedFlags { get; private set; }

    /// <summary>
    /// Indicates the number of times a player has returned the flag to their team's base.
    /// </summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int ReturnedFlags { get; private set; }

    /// <summary>
    /// Indicates the number of shots that the player has made at the heads of other players.
    /// </summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public int HeadShots { get; private set; }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-07 (GunGame mode rules) → CD-10; CD-20 (outbound repository contract) → CD-10</remarks>
    public int GunGameWins { get; private set; }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public RankId RankId { get; private set; } = RankId.Noob;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public DateTime LastConnection { get; private set; } = DateTime.UtcNow;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void SetLastConnection() => LastConnection = DateTime.UtcNow;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void SetMaxKillingSpree(int value) => MaxKillingSpree = value;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void AddTotalKills() => TotalKills++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void AddTotalDeaths() => TotalDeaths++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void AddBroughtFlags() => BroughtFlags++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void AddCapturedFlags() => CapturedFlags++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void AddDroppedFlags() => DroppedFlags++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void AddReturnedFlags() => ReturnedFlags++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void AddHeadShots() => HeadShots++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-07 (GunGame mode rules) → CD-10</remarks>
    public void AddGunGameWins() => GunGameWins++;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public Result SetTotalKills(int value)
    {
        if (value < 0)
            return Result.Failure(Messages.ValueCannotBeNegative);

        TotalKills = value;
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public Result SetTotalDeaths(int value)
    {
        if (value < 0)
            return Result.Failure(Messages.ValueCannotBeNegative);

        TotalDeaths = value;
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public Result SetRank(RankId id)
    {
        if (id < 0 || (int)id >= RankCollection.Count)
            return Result.Failure(Messages.InvalidRank);

        RankId = id;
        return Result.Success();
    }
}
