namespace CTF.Application.Commands.Basic.PublicCommandDialog;

/// <summary>
/// Provides the public command dialogs (help, credits, and the two command pages).
/// </summary>
/// <remarks>Injected dependency (change driver of this element): dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
public class PublicCommandDialog(IDialogService dialogService) : ISystem
{
    /// <summary>Shows the first page of public commands.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    [PlayerCommand("cmds")]
    public async Task ShowFirstCommandsPage(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Public1, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Commands [1/2]",
            content,
            button1: "Next",
            button2: "Close"
        );

        MessageDialogResponse response = await dialogService.ShowAsync(player, dialog);

        if (response.Response == DialogResponse.LeftButton)
            await ShowSecondCommandsPage(player);
    }

    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    private async Task ShowSecondCommandsPage(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Public2, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Commands [2/2]",
            content,
            button1: "Previous",
            button2: "Close"
        );

        MessageDialogResponse response = await dialogService.ShowAsync(player, dialog);

        if (response.Response == DialogResponse.LeftButton)
            await ShowFirstCommandsPage(player);
    }

    /// <summary>Shows the help dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    [PlayerCommand("help")]
    public void ShowHelp(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Help, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Help", 
            content, 
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    /// <summary>Shows the credits dialog.</summary>
    [ChangeDriversAttribute(ChangeDriver.CommandSet, ChangeDriver.Dialog)]
    [PlayerCommand("credits")]
    public void ShowCredits(Player player)
    {
        var content = Smart.Format(DetailedCommandInfo.Credits, new
        {
            Color1 = Color.Yellow,
            Color2 = Color.White
        });

        var dialog = new MessageDialog(
            caption: "Credits",
            content,
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }
}
