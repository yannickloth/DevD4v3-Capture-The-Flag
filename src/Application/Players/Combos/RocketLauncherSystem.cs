namespace CTF.Application.Players.Combos;

/// <remarks>Change drivers: CD-05 (combo definitions), CD-12 (map-rotation rules), CD-17 (game configuration/.env schema), CD-01 (open.mp/SampSharp platform API)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; comboSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class RocketLauncherSystem(
    IWorldService worldService,
    ComboSettings comboSettings) : ISystem
{
    [Event]
    /// <remarks>Change drivers: CD-12 (map-rotation rules), CD-01 (open.mp/SampSharp platform API)</remarks>
    public void OnLoadingMap()
    {
        comboSettings.IsRocketLauncherDisabled = true;
    }

    [PlayerCommand("rpgon")]
    [RequiresMinimumRole(RoleId.Moderator)]
    /// <remarks>Change drivers: CD-05 (combo definitions), CD-15 (command set), CD-17 (game configuration/.env schema), CD-01 (open.mp/SampSharp platform API)</remarks>
    public void EnableRocketLauncher(Player player)
    {
        var message = Smart.Format(Messages.EnableRocketLauncher, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        comboSettings.IsRocketLauncherDisabled = false;
    }

    [PlayerCommand("rpgoff")]
    [RequiresMinimumRole(RoleId.Moderator)]
    /// <remarks>Change drivers: CD-05 (combo definitions), CD-15 (command set), CD-17 (game configuration/.env schema), CD-01 (open.mp/SampSharp platform API)</remarks>
    public void DisableRocketLauncher(Player player)
    {
        var message = Smart.Format(Messages.DisableRocketLauncher, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        comboSettings.IsRocketLauncherDisabled = true;
    }
}
