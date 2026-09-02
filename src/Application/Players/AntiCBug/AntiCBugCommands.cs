namespace CTF.Application.Players.AntiCBug;

/// <summary>
/// Provides administrative commands to enable or disable the GTA: San Andreas
/// crouch bug (C-Bug) protection.
/// </summary>
/// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-17 (game configuration/.env schema) → CD-14; CD-01 (open.mp/SampSharp platform API) → CD-14</remarks>
/// <remarks>
/// C-Bug is a bug in GTA: San Andreas that allows players to manipulate the
/// reload animation of certain weapons, particularly the Desert Eagle, to fire
/// much faster than the game's normal mechanics would allow.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-01; antiCBugSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class AntiCBugCommands(
    IWorldService worldService,
    AntiCBugSettings antiCBugSettings) : ISystem
{
    /// <summary>Disables the anti-cheat protection for the C-Bug.</summary>
    /// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-17 (game configuration/.env schema) → CD-14; CD-01 (open.mp/SampSharp platform API) → CD-14</remarks>
    [PlayerCommand("anticbugoff")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void Disable(Player player)
    {
        var message = Smart.Format(Messages.DisableAntiCBug, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        antiCBugSettings.Disabled = true;
    }

    /// <summary>Enables the anti-cheat protection for the C-Bug.</summary>
    /// <remarks>Change drivers: CD-14 (root; anti-cheat policy); CD-17 (game configuration/.env schema) → CD-14; CD-01 (open.mp/SampSharp platform API) → CD-14</remarks>
    [PlayerCommand("anticbugon")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void Enable(Player player)
    {
        var message = Smart.Format(Messages.EnableAntiCBug, new
        {
            PlayerName = player.Name
        });
        worldService.SendClientMessage(Color.Yellow, message);
        antiCBugSettings.Disabled = false;
    }
}
