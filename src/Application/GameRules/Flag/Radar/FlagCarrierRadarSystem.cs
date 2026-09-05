namespace CTF.Application.GameRules.Flag.Radar;

/// <summary>
/// Handles showing and hiding flag carriers on the radar map.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): flagCarrierSettings -> CD-17; worldService -> CD-36. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.MapIcon, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Configuration)]
public class FlagCarrierRadarSystem(
    FlagCarrierSettings flagCarrierSettings,
    IWorldService worldService) : ISystem
{
    /// <summary>Shows all flag carriers on the radar map.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.MapIcon)]
    [PlayerCommand("showrm")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ShowOnRadarMap(Player player)
    {
        var message = Smart.Format(Messages.ShowFlagCarriersOnRadarMap, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        Team.Alpha.Flag.Carrier?.Player.ShowOnRadarMap();
        Team.Beta.Flag.Carrier?.Player.ShowOnRadarMap();
        flagCarrierSettings.ShowOnRadarMap = true;
    }

    /// <summary>Hides all flag carriers from the radar map.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.MapIcon)]
    [PlayerCommand("hiderm")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void HideOnRadarMap(Player player)
    {
        var message = Smart.Format(Messages.HideFlagCarriersOnRadarMap, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        Team.Alpha.Flag.Carrier?.Player.HideOnRadarMap();
        Team.Beta.Flag.Carrier?.Player.HideOnRadarMap();
        flagCarrierSettings.ShowOnRadarMap = false;
    }
}
