namespace CTF.Application.Commands.DialogNsNs3;

/// <summary>
/// Shows the admin commands dialog.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
public class ShowAdminCommandsSystem(
    IDialogService dialogService) : ISystem
{
    /// <summary>Shows the admin commands dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
    [PlayerCommand("cmdsadmin")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void ShowAdminCommands(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Admin, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Admin Commands", 
            content, 
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }
}
