namespace CTF.Application.Teams.Flags.Carriers;

/// <summary>
/// Handles showing and hiding flag carriers on the radar map.
/// </summary>
/// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier radar rule); CD-01 (open.mp/SampSharp platform API: radar) → CD-02; CD-15 (command set: showrm/hiderm commands) → CD-02; CD-09 (authorization policy: moderator gating) → CD-02; CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap) → CD-02</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): flagCarrierSettings -> CD-17; worldService -> CD-01. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class FlagCarrierRadarSystem(
    FlagCarrierSettings flagCarrierSettings,
    IWorldService worldService) : ISystem
{
    /// <summary>Shows all flag carriers on the radar map.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier radar rule); CD-15 (command set: showrm command) → CD-02; CD-09 (authorization policy: moderator gating) → CD-02; CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap) → CD-02; CD-01 (open.mp/SampSharp platform API: radar) → CD-02</remarks>
    [PlayerCommand("showrm")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ShowOnRadarMap(Player player)
    {
        var message = Smart.Format(Messages.ShowFlagCarriersOnRadarMap, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        Team.Alpha.Flag.Carrier?.ShowOnRadarMap();
        Team.Beta.Flag.Carrier?.ShowOnRadarMap();
        flagCarrierSettings.ShowOnRadarMap = true;
    }

    /// <summary>Hides all flag carriers from the radar map.</summary>
    /// <remarks>Change drivers: CD-02 (root; CTF game-rules specification: carrier radar rule); CD-15 (command set: hiderm command) → CD-02; CD-09 (authorization policy: moderator gating) → CD-02; CD-17 (game configuration/.env schema: FlagCarrier__ShowOnRadarMap) → CD-02; CD-01 (open.mp/SampSharp platform API: radar) → CD-02</remarks>
    [PlayerCommand("hiderm")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void HideOnRadarMap(Player player)
    {
        var message = Smart.Format(Messages.HideFlagCarriersOnRadarMap, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        Team.Alpha.Flag.Carrier?.HideOnRadarMap();
        Team.Beta.Flag.Carrier?.HideOnRadarMap();
        flagCarrierSettings.ShowOnRadarMap = false;
    }
}
