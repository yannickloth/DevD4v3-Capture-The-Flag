
namespace CTF.Application.Authorization.CommandSetNsNs3;

/// <summary>
/// Promotes the server owner to admin via a secret key.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; dialogService -> CD-33; serverOwnerSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
public class GiveMeAdminCommands(
    IPlayerRepository playerRepository,
    IDialogService dialogService,
    ServerOwnerSettings serverOwnerSettings) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Configuration, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
    [PlayerCommand("givemeadmin")]
    public async Task GiveMeAdmin(Player currentPlayer)
    {
        if (string.IsNullOrWhiteSpace(serverOwnerSettings.Name) ||
            string.IsNullOrWhiteSpace(serverOwnerSettings.SecretKey))
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.OwnerNameOrSecretKeyAreNotSet);
            return;
        }

        var ownerName = serverOwnerSettings.Name.Trim();
        bool isNotOwner = !currentPlayer.Name.Equals(ownerName, StringComparison.OrdinalIgnoreCase);
        if (isNotOwner)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsNotServerOwner);
            return;
        }

        var dialog = new InputDialog()
        {
            Caption = "Secret key",
            Content = "Enter secret key",
            Button1 = "Accept",
            Button2 = "Close"
        };

        InputDialogResponse response = await dialogService.ShowAsync(currentPlayer, dialog);
        if (response.IsRightButtonOrDisconnected())
            return;

        var enteredSecretKey = response.InputText;
        bool isWrongSecretKey = enteredSecretKey != serverOwnerSettings.SecretKey;
        if (isWrongSecretKey)
        {
            const int MaxFailedAttempts = 3;
            var failedAttemptCount = currentPlayer.GetComponent<FailedAttemptCountComponent>();
            failedAttemptCount ??= currentPlayer.AddComponent<FailedAttemptCountComponent>();
            failedAttemptCount.Value++;
            if (failedAttemptCount.Value == MaxFailedAttempts)
            {
                currentPlayer.Kick();
                return;
            }
            currentPlayer.SendClientMessage(Color.Red, Messages.WrongSecretKey);
            await GiveMeAdmin(currentPlayer);
            return;
        }

        var gameText = Smart.Format(Messages.PromotedToRole, new { RoleName = RoleId.Admin });
        PlayerInfo playerInfo = currentPlayer.GetRequiredInfo();
        playerInfo.Role.Set(RoleId.Admin);
        playerRepository.UpdateRole(playerInfo);
        currentPlayer.GameText(gameText, TimeSpan.FromSeconds(4), GameTextStyle.Style3);
        currentPlayer.GetComponent<FailedAttemptCountComponent>()?.Destroy();
    }
}
