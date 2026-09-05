namespace CTF.Application.Authorization.Roles.Commands;

/// <remarks>Injected dependencies (change drivers of these elements): playerRepository -> CD-20; dialogService -> CD-33; serverOwnerSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class PlayerRoleSystem(
    IPlayerRepository playerRepository,
    IDialogService dialogService,
    ServerOwnerSettings serverOwnerSettings) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Repository, ChangeDriver.Player, ChangeDriver.GameText, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
    [PlayerCommand("setrole")]
    [RequiresMinimumRole(RoleId.Admin)]
    public void SetRole(
        Player currentPlayer, 
        [CommandParameter(Name = "playerId")]Player targetPlayer, 
        int roleId)
    {
        if (currentPlayer == targetPlayer)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerIsEqualsToTargetPlayer);
            return;
        }

        if (targetPlayer.IsUnauthenticated())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.UnauthenticatedPlayer);
            return;
        }

        if (targetPlayer.IsServerOwner())
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.CannotPerformActionOnServerOwner);
            return;
        }

        PlayerInfo targetPlayerInfo = targetPlayer.GetRequiredInfo();
        RoleId newRoleId = (RoleId)roleId;
        RoleId oldRoleId = targetPlayerInfo.Role.Id;
        Result result = targetPlayerInfo.Role.Set(newRoleId);
        if (result.IsFailed)
        {
            currentPlayer.SendClientMessage(Color.Red, result.Message);
            return;
        }

        if (oldRoleId == newRoleId)
        {
            currentPlayer.SendClientMessage(Color.Red, Messages.PlayerAlreadyHasThatRole);
            return;
        }

        var gameText = newRoleId > oldRoleId ?
            Smart.Format(Messages.PromotedToRole, new { RoleName = newRoleId }) :
            Smart.Format(Messages.DemotedToRole,  new { RoleName = newRoleId });

        var message = Smart.Format(Messages.RoleSuccessfullyChanged, new
        {
            RoleName = newRoleId,
            PlayerName = targetPlayer.Name
        });

        playerRepository.UpdateRole(targetPlayerInfo);
        targetPlayer.GameText(gameText, TimeSpan.FromSeconds(4), GameTextStyle.Style3);
        currentPlayer.SendClientMessage(Color.Yellow, message);
    }

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

    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Ecs)]
    private class FailedAttemptCountComponent : Component
    {
        [ChangeDriversAttribute(ChangeDriver.Authorization)]
        public int Value { get; set; } = 0;
    }
}
