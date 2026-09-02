namespace CTF.Application.Teams.Flags.Events;

/// <summary>
/// This event occurs when a player has taken the flag from a position other than the base.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag take rule); CD-01 (open.mp/SampSharp platform API: pickups, radar, audio, GameText) → CD-02; CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; teamPickupService -> CD-29+CD-01; flagAutoReturnTimer -> CD-29+CD-02; flagCarrierSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class OnFlagTaken(
    IWorldService worldService,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer,
    FlagCarrierSettings flagCarrierSettings) : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (root; root; CTF game-rules specification: flag state machine)</remarks>
    public FlagStatus FlagStatus => FlagStatus.Taken;

    /// <summary>Handles the flag-taken event.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: flag take and auto-return rules); CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap) → CD-02; CD-01 (open.mp/SampSharp platform API: radar) → CD-02</remarks>
    public void Handle(Team team, Player player)
    {
        teamPickupService.DestroyFlag(team);
        team.Sounds.PlayFlagTakenSound();
        flagAutoReturnTimer.Stop(team);
        var message = Smart.Format(Messages.OnFlagTaken, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag taken!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        if (flagCarrierSettings.ShowOnRadarMap)
        {
            player.ShowOnRadarMap();
        }
    }
}
