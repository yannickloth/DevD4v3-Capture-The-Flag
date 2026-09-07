namespace CTF.Application.Commands.Help;

/// <summary>
/// Shows the moderator commands dialog.
/// </summary>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
public class ShowModeratorCommandsSystem : ISystem
{
    /// <summary>Shows the moderator commands dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Dialog)]
    [PlayerCommand("cmdsmoderator")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ShowModeratorCommands(Player player, IDialogService dialogService)
    {
        var content = Smart.Format(DetailedCommandInfo.Moderator, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Moderator Commands",
            content,
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }
}
