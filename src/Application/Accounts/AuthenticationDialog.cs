namespace CTF.Application.Accounts;

/// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-33 (dialog API) → CD-08</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): dialogService -> CD-33; accountAuthenticator -> CD-08. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class AuthenticationDialog(
    IDialogService dialogService,
    AccountAuthenticator accountAuthenticator)
{
    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-33 (dialog API) → CD-08</remarks>
    private readonly InputDialog _signupDialog = new()
    {
        IsPassword = true,
        Caption = "Signup",
        Content = "Enter a password",
        Button1 = "Accept"
    };

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-33 (dialog API) → CD-08</remarks>
    private readonly InputDialog _loginDialog = new()
    {
        IsPassword = true,
        Caption = "Login",
        Content = "Enter your password",
        Button1 = "Accept"
    };

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-33 (dialog API) → CD-08</remarks>
    public async Task ShowSignup(Player player)
    {
        InputDialogResponse response = await dialogService.ShowAsync(player, _signupDialog);
        if (response.Response == DialogResponse.Disconnected)
            return;

        if (response.Response == DialogResponse.RightButtonOrCancel)
        {
            await ShowSignup(player);
            return;
        }

        var enteredPassword = response.InputText ?? string.Empty;
        Result result = accountAuthenticator.Signup(player, enteredPassword);
        if (result.IsFailed)
            await ShowSignup(player);
    }

    /// <remarks>Change drivers: CD-08 (root; account & authentication policy); CD-33 (dialog API) → CD-08</remarks>
    public async Task ShowLogin(Player player)
    {
        InputDialogResponse response = await dialogService.ShowAsync(player, _loginDialog);
        if (response.Response == DialogResponse.Disconnected)
            return;

        if (response.Response == DialogResponse.RightButtonOrCancel)
        {
            await ShowLogin(player);
            return;
        }

        var enteredPassword = response.InputText ?? string.Empty;
        Result result = accountAuthenticator.Login(player, enteredPassword);
        if (result.IsFailed)
            await ShowLogin(player);
    }
}
