namespace CTF.Application.Commands.Vip.Help;

/// <summary>
/// Shows the VIP-role help dialog.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
public class VipHelpCommands : ISystem
{
    /// <summary>Shows the VIP commands dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
    [PlayerCommand("cmdsvip")]
    [RequiresMinimumRole(RoleId.VIP)]
    public void ShowVipCommands(Player player, IDialogService dialogService)
    {
        var content = Smart.Format(DetailedCommandInfo.VIP, new 
        { 
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "VIP Commands", 
            content, 
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }
}
