namespace CTF.Application.Teams.Flags.Events;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag and brought it back to their own base.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rule); CD-01 (open.mp/SampSharp platform API: pickups, audio, GameText, textdraw) → CD-02; CD-06 (coin economy: coins-on-flag-event) → CD-02; CD-10 (player-statistics/rank model: brought flags) → CD-02; CD-20 (outbound repository contract: UpdateBroughtFlags) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-01; teamPickupService -> CD-29+CD-01; teamTextDrawRenderer -> CD-29+CD-01; playerStatsRenderer -> CD-29+CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class OnFlagScore(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    TeamTextDrawRenderer teamTextDrawRenderer,
    PlayerStatsRenderer playerStatsRenderer) : IFlagEvent
{
    private const int CarrierEarnedCoins = 8;
    private const int CarrierEarnedScore = 4;
    private const int TeamEarnedCoins    = 5;
    private const int TeamEarnedHealth   = 10;
    private const int TeamEarnedScore    = 1;

    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    public FlagStatus FlagStatus => FlagStatus.Brought;

    /// <summary>Handles the flag-score event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rule); CD-06 (coin economy: coins-on-flag-event) → CD-02; CD-10 (player-statistics/rank model: brought flags) → CD-02; CD-20 (outbound repository contract: UpdateBroughtFlags) → CD-02</remarks>
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateFlagFromBasePosition(team.RivalTeam);
        teamPickupService.DestroyExteriorMarker(team.RivalTeam);
        team.Sounds.PlayTeamScoresSound();
        teamTextDrawRenderer.UpdateTeamScore(team);

        var message = Smart.Format(Messages.OnFlagScore, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.RivalTeam.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} team scores!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.StatsPerRound.AddCoins(CarrierEarnedCoins);
        playerInfo.AddBroughtFlags();
        player.AddScore(CarrierEarnedScore);
        player.HideOnRadarMap();
        playerRepository.UpdateBroughtFlags(playerInfo);
        GiveRewards(team);
    }

    private void GiveRewards(Team team)
    {
        TeamMembers teamMembers = team.Members;
        foreach (Player player in teamMembers)
        {
            PlayerInfo playerInfo = player.GetRequiredInfo();
            playerInfo.StatsPerRound.AddCoins(TeamEarnedCoins);
            player.AddHealth(TeamEarnedHealth);
            player.AddScore(TeamEarnedScore);
            playerStatsRenderer.UpdateTextDraw(player);
        }
    }
}
