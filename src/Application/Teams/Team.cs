namespace CTF.Application.GameRules.AudioDomain;


/// <summary>
/// Represents a team in the CTF gamemode, holding its identity, members, stats, and flag.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
public class Team 
{
    /// <summary>Gets the Alpha team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public static readonly Team Alpha;
    /// <summary>Gets the Beta team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public static readonly Team Beta;
    /// <summary>Gets the NoTeam placeholder team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public static readonly Team None;
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team instantiation)</remarks>
    private Team() { }

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: team identity initialization); CD-31 (team id/skin/color, TeamSounds contract) → CD-02</remarks>
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
            Sounds        = new TeamSounds(TeamSoundCatalog.Alpha),
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
            Sounds        = new TeamSounds(TeamSoundCatalog.Beta),
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
            Sounds        = new TeamSounds(TeamSoundCatalog.None),
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
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public TeamId Id { get; private set; }
    /// <summary>Gets the team skin identifier.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public SkinTeamId SkinId { get; private set; }
    /// <summary>Gets the team name.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public string Name { get; private set; }
    /// <summary>Gets the team color name.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public string ColorName { get; private set; }

    /// <summary>
    /// Gets the text color used by open.mp <c>GameText</c>.
    /// </summary>
    /// <remarks>
    /// See the <see href="https://open.mp/docs/scripting/resources/gametextstyles#text-colors">
    /// open.mp GameText text colors documentation
    /// </see>.
    /// </remarks>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public string GameTextColor { get; private set; }

    /// <summary>Gets the team color in hexadecimal.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public Color ColorHex { get; private set; }
    /// <summary>Gets the sounds associated with the team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public TeamSounds Sounds { get; private set; }
    /// <summary>Gets the team's flag.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public Flag Flag { get; private set; }
    /// <summary>Gets the rival team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public Team RivalTeam { get; private set; }
    /// <summary>Gets the team members.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public TeamMembers Members { get; } = [];
    /// <summary>Gets the per-round statistics for the team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public TeamStatsPerRound StatsPerRound { get; } = new();

    /// <summary>Gets the team member count as text.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public virtual string GetMembersAsText() => $"{Members.Count}";
    /// <summary>Checks whether the team has more members than its rival.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public virtual bool IsFull() => Members.Count > RivalTeam.Members.Count;
    /// <summary>Checks whether the team has a higher score than its rival.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public virtual bool IsWinner() => StatsPerRound.Score > RivalTeam.StatsPerRound.Score;
    /// <summary>Resets the team's round state.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
    public virtual void Reset()
    {
        StatsPerRound.Reset();
        Members.Clear();
        Flag.Reset();
    }

    /// <summary>Gets the team availability message.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
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
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player, ChangeDriver.Model, ChangeDriver.Statistics, ChangeDriver.Configuration, ChangeDriver.TextDraw, ChangeDriver.GameText, ChangeDriver.Audio)]
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

            if (RivalTeam.Flag.Carrier?.Is(flagPicker) == true)
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

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
    private class NoTeam : Team
    {
        /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: the None team)</remarks>
        public NoTeam() { }
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
        public override string GetAvailabilityMessage() => string.Empty;
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
        public override FlagStatus HandleFlagInteraction(Player flagPicker) => FlagStatus.BasePosition;
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
        public override string GetMembersAsText() => string.Empty;
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
        public override bool IsFull() => false;
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
        public override bool IsWinner() => false;
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Player)]
        public override void Reset()
        {
            StatsPerRound.Reset();
            Members.Clear();
            Flag.Reset();
        }
    }
}
