namespace CTF.Application.Teams.Flags.Events;

/// <summary>
/// This event occurs when a player has taken the flag from a position other than the base.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag take rule), CD-01 (open.mp/SampSharp platform API: pickups, radar, audio, GameText), CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap).</remarks>
public class OnFlagTaken(
    IWorldService worldService,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer,
    FlagCarrierSettings flagCarrierSettings) : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag state machine).</remarks>
    public FlagStatus FlagStatus => FlagStatus.Taken;

    /// <summary>Handles the flag-taken event.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: flag take and auto-return rules), CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap), CD-01 (open.mp/SampSharp platform API: radar).</remarks>
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
