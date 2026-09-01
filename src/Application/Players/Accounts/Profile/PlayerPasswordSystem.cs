namespace CTF.Application.Players.Accounts.Profile;

/// <remarks>Change drivers: CD-08 (account & authentication policy), CD-01 (open.mp/SampSharp platform API), CD-20 (outbound repository contract)</remarks>
public class PlayerPasswordSystem(
    IPlayerRepository playerRepository,
    IDialogService dialogService) : ISystem
{
    /// <remarks>Change drivers: CD-08 (account & authentication policy), CD-01 (open.mp/SampSharp platform API)</remarks>
    private readonly InputDialog _passwordDialog = new()
    {
        IsPassword = true,
        Caption = "Change Password",
        Content = "Enter your new password",
        Button1 = "Accept",
        Button2 = "Close"
    };

    /// <remarks>Change drivers: CD-08 (account & authentication policy), CD-01 (open.mp/SampSharp platform API)</remarks>
    [PlayerCommand("changepass")]
    public async Task ShowPasswordDialog(Player player)
    {
        InputDialogResponse response = await dialogService.ShowAsync(player, _passwordDialog);
        if (response.IsRightButtonOrDisconnected())
            return;

        var enteredPassword = response.InputText ?? string.Empty;
        await ChangePassword(player, enteredPassword);
    }

    /// <remarks>Change drivers: CD-08 (account & authentication policy), CD-20 (outbound repository contract)</remarks>
    private async Task ChangePassword(Player player, string enteredPassword)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        Result result = playerInfo.SetPassword(enteredPassword);
        if (result.IsFailed)
        {
            player.SendClientMessage(Color.Red, result.Message);
            await ShowPasswordDialog(player);
            return;
        }

        var message = Smart.Format(Messages.PasswordSuccessfullyChanged, new { NewPassword = enteredPassword });
        player.SendClientMessage(Color.Yellow, message);
        playerRepository.UpdatePassword(playerInfo);
    }
}
