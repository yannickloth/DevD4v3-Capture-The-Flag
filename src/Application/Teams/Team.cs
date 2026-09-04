namespace CTF.Application.Teams;

/// <summary>
/// Represents a team in the CTF gamemode, holding its identity, members, stats, and flag.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team identity and balance) ‖ CD-01 (root; open.mp/SampSharp platform API: player team/color/skin)</remarks>
public class Team 
{
    /// <summary>Gets the Alpha team.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balance)</remarks>
    public static readonly Team Alpha;
    /// <summary>Gets the Beta team.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balance)</remarks>
    public static readonly Team Beta;
    /// <summary>Gets the NoTeam placeholder team.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: no-team state)</remarks>
    public static readonly Team None;
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team instantiation)</remarks>
    private Team() { }

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team identity initialization); CD-01 (open.mp/SampSharp platform API: team id/skin/color, TeamSounds contract) → CD-02</remarks>
    static Team() 
    {
        Alpha = new Team
        {
            Id            = TeamId.Alpha,
            SkinId        = SkinTeamId.Alpha,
            Name          = "Alpha",
            ColorName     = "red",
            GameTextColor = "~r~",
            ColorHex      = new Color(255, 32, 64, 00),
            Sounds        = TeamSounds.Alpha,
            Flag          = new Flag
            {
                Model     = FlagModel.Red,
                Icon      = FlagIcon.Red,
                Name      = "Red",
                ColorHex  = Color.Red
            }
        };

        Beta = new Team
        {
            Id            = TeamId.Beta,
            SkinId        = SkinTeamId.Beta,
            Name          = "Beta",
            ColorName     = "blue",
            GameTextColor = "~b~",
            ColorHex      = new Color(0, 136, 255, 00),
            Sounds        = TeamSounds.Beta,
            Flag          = new Flag
            {
                Model     = FlagModel.Blue,
                Icon      = FlagIcon.Blue,
                Name      = "Blue",
                ColorHex = Color.Blue
            }
        };

        Alpha.RivalTeam = Beta;
        Beta.RivalTeam  = Alpha;
        None = new NoTeam
        {
            Id            = TeamId.NoTeam,
            SkinId        = SkinTeamId.NoTeam,
            Name          = "NoTeam",
            ColorName     = "white",
            GameTextColor = "~w~",
            ColorHex      = new Color(255, 255, 255, 00),
            Sounds        = TeamSounds.None,
            Flag          = new Flag
            {
                Model     = FlagModel.None,
                Icon      = FlagIcon.White,
                Name      = "NoTeam",
                ColorHex  = Color.White
            },
        };
        None.RivalTeam = None;
    }

    /// <summary>Gets the team identifier.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: team id) ‖ CD-02 (root; CTF game-rules specification: team identity)</remarks>
    public TeamId Id { get; private set; }
    /// <summary>Gets the team skin identifier.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: skin id)</remarks>
    public SkinTeamId SkinId { get; private set; }
    /// <summary>Gets the team name.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team identity)</remarks>
    public string Name { get; private set; }
    /// <summary>Gets the team color name.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team color identity)</remarks>
    public string ColorName { get; private set; }

    /// <summary>
    /// Gets the text color used by open.mp <c>GameText</c>.
    /// </summary>
    /// <remarks>
    /// See the <see href="https://open.mp/docs/scripting/resources/gametextstyles#text-colors">
    /// open.mp GameText text colors documentation
    /// </see>.
    /// </remarks>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: GameText text color) ‖ CD-02 (root; CTF game-rules specification: team color identity)</remarks>
    public string GameTextColor { get; private set; }

    /// <summary>Gets the team color in hexadecimal.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: color value) ‖ CD-02 (root; CTF game-rules specification: team color identity)</remarks>
    public Color ColorHex { get; private set; }
    /// <summary>Gets the sounds associated with the team.</summary>
    /// <remarks>Change drivers: CD-01 (root; open.mp/SampSharp platform API: audio); CD-17 (game configuration/.env schema: audio URLs) → CD-01</remarks>
    public TeamSounds Sounds { get; private set; }
    /// <summary>Gets the team's flag.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag ownership)</remarks>
    public Flag Flag { get; private set; }
    /// <summary>Gets the rival team.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team pairing)</remarks>
    public Team RivalTeam { get; private set; }
    /// <summary>Gets the team members.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team membership)</remarks>
    public TeamMembers Members { get; } = [];
    /// <summary>Gets the per-round statistics for the team.</summary>
    /// <remarks>Change drivers: CD-10 (root; player-statistics/rank model: team stats)</remarks>
    public TeamStatsPerRound StatsPerRound { get; } = new();

    /// <summary>Gets the team member count as text.</summary>
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API: textdraw) → CD-02; CD-02 (root; CTF game-rules specification: team membership)</remarks>
    public virtual string GetMembersAsText() => $"{Members.Count}";
    /// <summary>Checks whether the team has more members than its rival.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balancing)</remarks>
    public virtual bool IsFull() => Members.Count > RivalTeam.Members.Count;
    /// <summary>Checks whether the team has a higher score than its rival.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: match end conditions)</remarks>
    public virtual bool IsWinner() => StatsPerRound.Score > RivalTeam.StatsPerRound.Score;
    /// <summary>Resets the team's round state.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: round/flag reset rule)</remarks>
    public virtual void Reset()
    {
        StatsPerRound.Reset();
        Members.Clear();
        Flag.Reset();
    }

    /// <summary>Gets the team availability message.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team balancing availability)</remarks>
    public virtual string GetAvailabilityMessage()
        => IsFull() ? 
        $"~y~{Name}~n~~r~ not available" : 
        $"~y~{Name}~n~~r~ available";

    /// <summary>
    /// Handles a player's interaction with the team's flag.
    /// </summary>
    /// <param name="flagPicker">
    /// The player interacting with the team's flag.
    /// </param>
    /// <returns>
    /// The status resulting from the interaction.
    /// </returns>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag steal/capture/return rules); CD-01 (open.mp/SampSharp platform API: player team/entity) → CD-02</remarks>
    public virtual FlagStatus HandleFlagInteraction(Player flagPicker)
    {
        ArgumentNullException.ThrowIfNull(flagPicker);
        if (Flag.Status == FlagStatus.BasePosition)
        {
            if (flagPicker.Team == (int)RivalTeam.Id)
            {
                Flag.Capture(flagPicker);
                return FlagStatus.Captured;
            }

            if (flagPicker == RivalTeam.Flag.Carrier)
            {
                RivalTeam.Flag.ReturnToBase();
                StatsPerRound.AddScore();
                return FlagStatus.Brought;
            }

            return FlagStatus.BasePosition;
        }

        if (flagPicker.Team == (int)Id)
        {
            Flag.ReturnToBase();
            return FlagStatus.Returned;
        }

        Flag.Take(flagPicker);
        return FlagStatus.Taken;
    }

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team identity, the None team)</remarks>
    private class NoTeam : Team
    {
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team)</remarks>
        public NoTeam() { }
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team, no availability message)</remarks>
        public override string GetAvailabilityMessage() => string.Empty;
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team takes no flag interaction); CD-01 (open.mp/SampSharp platform API: player entity) → CD-02</remarks>
        public override FlagStatus HandleFlagInteraction(Player flagPicker) => FlagStatus.BasePosition;
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team has no member text)</remarks>
        public override string GetMembersAsText() => string.Empty;
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team is never full)</remarks>
        public override bool IsFull() => false;
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team never wins)</remarks>
        public override bool IsWinner() => false;
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team round/flag reset rule)</remarks>
        public override void Reset()
        {
            StatsPerRound.Reset();
            Members.Clear();
            Flag.Reset();
        }
    }
}
