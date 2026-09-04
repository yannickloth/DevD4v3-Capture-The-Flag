namespace CTF.Application.Commands;

/// <summary>
/// Shows the VIP-role help dialog.
/// </summary>
/// <remarks>Change drivers: CD-15 (root; command set); CD-09 (authorization policy) → CD-15; CD-33 (dialog) → CD-15</remarks>
public class VipHelpCommands : ISystem
{
    /// <summary>Shows the VIP commands dialog.</summary>
    /// <remarks>Change drivers: CD-15 (root; command set); CD-09 (authorization policy) → CD-15; CD-33 (dialog) → CD-15</remarks>
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
