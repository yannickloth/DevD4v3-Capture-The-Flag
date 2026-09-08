namespace CTF.Application.Combos.ClientMessageDomain;

/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; comboSettings -> CD-05. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.MapRotation, ChangeDriver.ClientMessage)]
public class RocketLauncherSystem(
    IWorldService worldService,
    ComboSettings comboSettings) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.MapRotation, ChangeDriver.ClientMessage)]
    public void OnLoadingMap()
    {
        comboSettings.IsRocketLauncherDisabled = true;
    }

    [PlayerCommand("rpgon")]
    [RequiresMinimumRole(RoleId.Moderator)]
    [ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.MapRotation, ChangeDriver.ClientMessage)]
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
    [ChangeDriversAttribute(ChangeDriver.Combo, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.MapRotation, ChangeDriver.ClientMessage)]
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
