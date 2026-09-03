namespace CTF.Application.GameRules;

/// <summary>
/// This event occurs when a player has returned the flag to their team's base.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag return rule); CD-01 (open.mp/SampSharp platform API: pickups, audio, GameText) → CD-02; CD-06 (coin economy: coins-on-flag-event) → CD-02; CD-10 (player-statistics/rank model: returned flags) → CD-02; CD-20 (outbound repository contract: UpdateReturnedFlags) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-01; teamPickupService -> CD-01; playerStatsRenderer -> CD-10; flagAutoReturnTimer -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class OnFlagReturned(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    PlayerStatsRenderer playerStatsRenderer,
    FlagAutoReturnTimer flagAutoReturnTimer) : IFlagEvent
{
    private const int EarnedCoins = 5;
    private const int EarnedScore = 2;

    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag state machine)</remarks>
    public FlagStatus FlagStatus => FlagStatus.Returned;

    /// <summary>Handles the flag-returned event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag return rule); CD-06 (coin economy: coins-on-flag-event) → CD-02; CD-10 (player-statistics/rank model: returned flags) → CD-02; CD-20 (outbound repository contract: UpdateReturnedFlags) → CD-02</remarks>
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateFlagFromBasePosition(team);
        teamPickupService.DestroyExteriorMarker(team);
        team.Sounds.PlayFlagReturnedSound();
        flagAutoReturnTimer.Stop(team);
        var message = Smart.Format(Messages.OnFlagReturned, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag returned!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.StatsPerRound.AddCoins(EarnedCoins);
        playerInfo.AddReturnedFlags();
        player.AddScore(EarnedScore);
        playerRepository.UpdateReturnedFlags(playerInfo);
        playerStatsRenderer.UpdateTextDraw(player);
    }
}
