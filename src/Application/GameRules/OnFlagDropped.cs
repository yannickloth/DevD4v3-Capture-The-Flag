namespace CTF.Application.GameRules;

/// <summary>
/// This event occurs when a player has dropped the opposing team's flag.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag drop rule); CD-35; CD-37; CD-38; CD-40 (pickups, radar, audio, GameText) → CD-02; CD-10 (player-statistics/rank model: dropped flags) → CD-02; CD-20 (outbound repository contract: UpdateDroppedFlags) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; worldService -> CD-36; teamPickupService -> CD-37; flagAutoReturnTimer -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class OnFlagDropped(
    IPlayerRepository playerRepository,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer) : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag state machine)</remarks>
    public FlagStatus FlagStatus => FlagStatus.Dropped;

    /// <summary>Handles the flag-dropped event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag drop and auto-return rules); CD-10 (player-statistics/rank model: dropped flags) → CD-02; CD-20 (outbound repository contract: UpdateDroppedFlags) → CD-02</remarks>
    public void Handle(Team team, Player player)
    {
        teamPickupService.CreateFlagFromVector3(team, player.Position);
        team.Sounds.PlayFlagDroppedSound();
        flagAutoReturnTimer.Start(team);
        team.Flag.Drop();
        var message = Smart.Format(Messages.OnFlagDropped, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag dropped!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);

        PlayerInfo playerInfo = player.GetRequiredInfo();
        playerInfo.Stats.AddDroppedFlags();
        player.HideOnRadarMap();
        playerRepository.UpdateDroppedFlags(playerInfo);
    }
}
