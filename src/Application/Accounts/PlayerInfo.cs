namespace CTF.Application.Accounts;

/// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-09 (authorization policy) → CD-08; CD-10 (player-statistics/rank model) → CD-08; CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
public partial class PlayerInfo
{
    /// <remarks>Change drivers: CD-08 (root; account & authentication policy: player name validation pattern)</remarks>
    private const string PlayerNamePattern = @"^[0-9a-zA-Z\[\]\(\)\$\@._=]+$";

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy: no-skin sentinel); CD-01 (open.mp/SampSharp platform API: skin id) → CD-08</remarks>
    private const int NoSkin = -1;

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy: no-account sentinel)</remarks>
    private const int NoAccount = -1;

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy: player name validation regex)</remarks>
    [GeneratedRegex(PlayerNamePattern)]
    private static partial Regex PlayerNameRegex();

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-09 (authorization policy) → CD-08; CD-10 (player-statistics/rank model) → CD-08; CD-01 (open.mp/SampSharp platform API) → CD-08</remarks>
    public PlayerInfo() { }

    /// <summary>
    /// It is generated automatically by the database provider.
    /// </summary>
    /// <remarks>
    /// It is a permanent identifier that is generated when the player's account is created in the database.
    /// </remarks>
    /// <remarks>Change drivers: CD-18 (database schema/player data model) ‖ CD-20 (outbound repository contract); both → CD-08 (account)</remarks>
    public int AccountId { get; private set; } = NoAccount;

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    public string Name { get; private set; } = "DefaultUser";

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-20 (outbound repository contract) → CD-08</remarks>
    public string Password { get; private set; } = "DefaultPassword";

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public PlayerStatsPerRound StatsPerRound { get; } = new();

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

    /// <remarks>Change drivers: CD-09 (root; authorization policy); CD-20 (outbound repository contract) → CD-09</remarks>
    public RoleId RoleId { get; private set; } = RoleId.Basic;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API); CD-20 (outbound repository contract) → CD-01</remarks>
    public int SkinId { get; private set; } = NoSkin;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public RankId RankId { get; private set; } = RankId.Noob;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public Team Team { get; private set; } = Team.None;

    /// <remarks>Change drivers: CD-18 (database schema/player data model) ‖ CD-20 (outbound repository contract); both → CD-08 (account)</remarks>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model); CD-20 (outbound repository contract) → CD-10</remarks>
    public DateTime LastConnection { get; private set; } = DateTime.UtcNow;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public bool HasSurpassedMaxKillingSpree() => StatsPerRound.KillingSpree > MaxKillingSpree;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public bool HasSkin() => SkinId != NoSkin;

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool HasRole(RoleId id) => RoleId == id;

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool HasLowerRoleThan(RoleId id) => RoleId < id;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public bool HasRank(RankId id) => RankId == id;

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool IsVIP() => HasRole(RoleId.VIP);

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool IsModerator() => HasRole(RoleId.Moderator);

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool IsAdmin() => HasRole(RoleId.Admin);

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool IsNotVIP() => !IsVIP();

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool IsNotModerator() => !IsModerator();

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public bool IsNotAdmin() => !IsAdmin();

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void SetLastConnection() => LastConnection = DateTime.UtcNow;

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public void SetMaxKillingSpree(int value) => MaxKillingSpree = value;

    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public void RemoveSkin() => SkinId = NoSkin;

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

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy)</remarks>
    public Result SetName(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure(Messages.NameCannotBeEmpty);

        if (value.Length < 3 || value.Length > 20)
            return Result.Failure(Messages.PlayerNameLength);

        if (!PlayerNameRegex().IsMatch(value))
            return Result.Failure(Messages.InvalidNickName);

        Name = value;
        return Result.Success();
    }

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy)</remarks>
    public Result SetPassword(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure(Messages.PasswordCannotBeEmpty);

        if (value.Length < 5 || value.Length > 20)
            return Result.Failure(Messages.PasswordLength);

        Password = value;
        return Result.Success();
    }

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

    /// <remarks>Change drivers: CD-09 (root; authorization policy)</remarks>
    public Result SetRole(RoleId id)
    {
        if (id < 0 || (int)id >= RoleCollection.Count)
            return Result.Failure(Messages.InvalidRole);

        RoleId = id;
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

    /// <summary>
    /// Checks if the player has captured the opposing team's flag.
    /// </summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API)</remarks>
    public bool IsCarryingEnemyFlag()
    {
        if (Team == Team.None) 
            return false;

        Flag rivalTeamFlag = Team.RivalTeam.Flag;
        if (rivalTeamFlag.HasCarrier)
        {
            Player carrier = rivalTeamFlag.Carrier;
            return carrier.Name.Equals(Name, StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public bool CanMoveUpToNextRank()
    {
        Rank currentRank = RankCollection.GetById(RankId).Value;
        if (currentRank.IsMax())
            return false;

        Rank nextRank = RankCollection.GetNextRank(RankId).Value;
        return TotalKills >= nextRank.RequiredKills;
    }

    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model)</remarks>
    public string GetStatsAsText()
    {
        Result<Rank> rankResult = RankCollection.GetById(RankId);
        var stats = new
        {
            StatsPerRound.Kills,
            StatsPerRound.Deaths,
            StatsPerRound.KillingSpree,
            StatsPerRound.Coins,
            MaxRank = RankCollection.Count,
            Level = (int)RankId + 1,
            RankName = rankResult.Value.Name
        };
        const string message = 
            "~w~KILLS: ~y~{Kills} ~w~DEATHS: ~y~{Deaths} ~w~SPREE: ~y~{KillingSpree} " +
            "~w~COINS: ~y~{Coins}/100 ~w~LEVEL: ~y~{Level}/{MaxRank} ~w~RANK: ~y~{RankName}";
        return Smart.Format(message, stats);
    }
}
