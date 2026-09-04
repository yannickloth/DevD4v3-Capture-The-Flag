namespace CTF.Application.GameRules;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag and brought it back to their own base.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rule); CD-34; CD-35; CD-37; CD-40 (pickups, audio, GameText, textdraw) → CD-02; CD-06 (coin economy: coins-on-flag-event) → CD-02; CD-10 (player-statistics/rank model: brought flags) → CD-02; CD-20 (outbound repository contract: UpdateBroughtFlags) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; teamTextDrawRenderer -> CD-34; playerStatsRenderer -> CD-10. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class OnFlagScore(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    TeamTextDrawRenderer teamTextDrawRenderer,
    PlayerStatsRenderer playerStatsRenderer) : IFlagEvent
{
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rewards)</remarks>
    private const int CarrierEarnedCoins = 8;

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rewards)</remarks>
    private const int CarrierEarnedScore = 4;

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rewards)</remarks>
    private const int TeamEarnedCoins    = 5;

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rewards)</remarks>
    private const int TeamEarnedHealth   = 10;

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score rewards)</remarks>
    private const int TeamEarnedScore    = 1;

    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag state machine)</remarks>
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
        playerInfo.Stats.PerRound.AddCoins(CarrierEarnedCoins);
        playerInfo.Stats.AddBroughtFlags();
        player.AddScore(CarrierEarnedScore);
        player.HideOnRadarMap();
        playerRepository.UpdateBroughtFlags(playerInfo);
        GiveRewards(team);
    }

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag score team rewards); CD-06 (coin economy) → CD-02; CD-10 (player-statistics/rank model) → CD-02; CD-31 (player entity: health/score/team members); CD-34 (stats textdraw) → CD-02</remarks>
    private void GiveRewards(Team team)
    {
        TeamMembers teamMembers = team.Members;
        foreach (Player player in teamMembers)
        {
            PlayerInfo playerInfo = player.GetRequiredInfo();
            playerInfo.Stats.PerRound.AddCoins(TeamEarnedCoins);
            player.AddHealth(TeamEarnedHealth);
            player.AddScore(TeamEarnedScore);
            playerStatsRenderer.UpdateTextDraw(player);
        }
    }
}
