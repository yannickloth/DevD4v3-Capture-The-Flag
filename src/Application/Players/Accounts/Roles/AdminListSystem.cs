namespace CTF.Application.Players.Accounts.Roles;

/// <remarks>Change drivers: CD-09 (root; authorization policy); CD-01 (open.mp/SampSharp platform API) → CD-09</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): dialogService -> CD-01; entityManager -> CD-01; serverOwnerSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class AdminListSystem(
    IDialogService dialogService,
    IEntityManager entityManager,
    ServerOwnerSettings serverOwnerSettings) : ISystem
{
    /// <remarks>Change drivers: CD-09 (root; authorization policy); CD-01 (open.mp/SampSharp platform API) → CD-09</remarks>
    [PlayerCommand("admins")]
    public void Show(Player player)
    {
        List<PlayerInfo> admins = entityManager
            .GetComponents<Player>()
            .Select(player => player.GetRequiredInfo())
            .Where(info => info.RoleId >= RoleId.Moderator)
            .OrderByDescending(IsServerOwner)
            .ThenByDescending(info => info.RoleId)
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
                content.AppendLine($"{ownerColor}[Server Owner] {Color.White}{admin.Name}");
                continue;
            }

            Color color = admin.RoleId switch
            {
                >= RoleId.Admin => Color.Red,
                >= RoleId.Moderator => Color.LightGreen,
                _ => Color.White
            };

            content.AppendLine($"{color}[{admin.RoleId}] {Color.White}{admin.Name}");
        }

        var dialog = new MessageDialog(
            caption: $"Admins: {admins.Count}",
            content: content.ToString(),
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }

    /// <remarks>Change drivers: CD-09 (root; root; authorization policy)</remarks>
    private bool IsServerOwner(PlayerInfo playerInfo)
        => playerInfo.Name.Equals(serverOwnerSettings.Name, StringComparison.OrdinalIgnoreCase);
}
