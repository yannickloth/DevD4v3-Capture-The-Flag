namespace CTF.Application.Authorization.Admin.Commands;

/// <remarks>Injected dependencies (change drivers of these elements): dialogService -> CD-33; entityManager -> CD-32; serverOwnerSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class AdminListSystem(
    IDialogService dialogService,
    IEntityManager entityManager,
    ServerOwnerSettings serverOwnerSettings) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
    [PlayerCommand("admins")]
    public void Show(Player player)
    {
        List<PlayerInfo> admins = entityManager
            .GetComponents<Player>()
            .Select(player => player.GetRequiredInfo())
            .Where(info => info.Role.Id >= RoleId.Moderator)
            .OrderByDescending(IsServerOwner)
            .ThenByDescending(info => info.Role.Id)
            .ToList();

        if (admins.Count == 0)
        {
            player.SendClientMessage(Color.Red, Messages.NoAdminsConnected);
            return;
        }

        var content = new StringBuilder();
        Color ownerColor = Color.Gold;

        foreach (PlayerInfo admin in admins)
        {
            if (IsServerOwner(admin))
            {
                content.AppendLine($"{ownerColor}[Server Owner] {Color.White}{admin.Account.Name}");
                continue;
            }

            Color color = admin.Role.Id switch
            {
                >= RoleId.Admin => Color.Red,
                >= RoleId.Moderator => Color.LightGreen,
                _ => Color.White
            };

            content.AppendLine($"{color}[{admin.Role.Id}] {Color.White}{admin.Account.Name}");
        }

        var dialog = new MessageDialog(
            caption: $"Admins: {admins.Count}",
            content: content.ToString(),
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandSet, ChangeDriver.Configuration, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
    private bool IsServerOwner(PlayerInfo playerInfo)
        => playerInfo.Account.Name.Equals(serverOwnerSettings.Name, StringComparison.OrdinalIgnoreCase);
}
