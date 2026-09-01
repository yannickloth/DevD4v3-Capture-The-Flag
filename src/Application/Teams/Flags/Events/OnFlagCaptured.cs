namespace CTF.Application.Teams.Flags.Events;

/// <summary>
/// This event occurs when a player has captured the opposing team's flag from their base.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag capture rule), CD-01 (open.mp/SampSharp platform API: pickups, radar, audio, GameText), CD-06 (coin economy: coins-on-flag-event), CD-10 (player-statistics/rank model: captured flags), CD-20 (outbound repository contract: UpdateCapturedFlags), CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap).</remarks>
public class OnFlagCaptured(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    PlayerStatsRenderer playerStatsRenderer,
    FlagCarrierSettings flagCarrierSettings) : IFlagEvent
{
    private const int EarnedCoins = 5;
    private const int EarnedScore = 2;

    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag state machine).</remarks>
    public FlagStatus FlagStatus => FlagStatus.Captured;

    /// <summary>Handles the flag-captured event.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag capture rule), CD-06 (coin economy: coins-on-flag-event), CD-10 (player-statistics/rank model: captured flags), CD-20 (outbound repository contract: UpdateCapturedFlags).</remarks>
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
        playerInfo.StatsPerRound.AddCoins(EarnedCoins);
        playerInfo.AddCapturedFlags();
        player.AddScore(EarnedScore);
        if (flagCarrierSettings.ShowOnRadarMap)
        {
            player.ShowOnRadarMap();
        }
        playerRepository.UpdateCapturedFlags(playerInfo);
        playerStatsRenderer.UpdateTextDraw(player);
    }
}
