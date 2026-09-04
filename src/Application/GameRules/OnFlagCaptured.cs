namespace CTF.Application.GameRules;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag from their base.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag capture rule); CD-35; CD-37; CD-38; CD-40 (pickups, radar, audio, GameText) → CD-02; CD-06 (coin economy: coins-on-flag-event) → CD-02; CD-10 (player-statistics/rank model: captured flags) → CD-02; CD-20 (outbound repository contract: UpdateCapturedFlags) → CD-02; CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; playerStatsRenderer -> CD-10; flagCarrierSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class OnFlagCaptured(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    PlayerStatsRenderer playerStatsRenderer,
    FlagCarrierSettings flagCarrierSettings) : IFlagEvent
{
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag capture rewards); CD-06 (coin economy) → CD-02</remarks>
    private const int EarnedCoins = 5;

    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag capture rewards); CD-10 (player-statistics/rank model) → CD-02</remarks>
    private const int EarnedScore = 2;

    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag state machine)</remarks>
    public FlagStatus FlagStatus => FlagStatus.Captured;

    /// <summary>Handles the flag-captured event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag capture rule); CD-06 (coin economy: coins-on-flag-event) → CD-02; CD-10 (player-statistics/rank model: captured flags) → CD-02; CD-20 (outbound repository contract: UpdateCapturedFlags) → CD-02</remarks>
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateExteriorMarker(team);
        teamPickupService.DestroyFlag(team);
        team.Sounds.PlayFlagTakenSound();
        var message = Smart.Format(Messages.OnFlagCaptured, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag captured!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.PerRound.AddCoins(EarnedCoins);
        playerInfo.Stats.AddCapturedFlags();
        player.AddScore(EarnedScore);
        if (flagCarrierSettings.ShowOnRadarMap)
        {
            player.ShowOnRadarMap();
        }
        playerRepository.UpdateCapturedFlags(playerInfo);
        playerStatsRenderer.UpdateTextDraw(player);
    }
}
