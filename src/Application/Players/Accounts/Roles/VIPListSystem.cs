namespace CTF.Application.Players.Accounts.Roles;

/// <remarks>Change drivers: CD-09 (root; authorization policy); CD-01 (open.mp/SampSharp platform API) → CD-09</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-01; dialogService -> CD-01. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class VIPListSystem(
    IEntityManager entityManager,
    IDialogService dialogService) : ISystem
{
/// <remarks>Change drivers: CD-09 (root; authorization policy); CD-15 (command set) → CD-09; CD-01 (open.mp/SampSharp platform API) → CD-09</remarks>
    [PlayerCommand("vips")]
    public void Show(Player player)
    {
        List<PlayerInfo> vips = entityManager
            .GetComponents<Player>()
            .Select(player => player.GetRequiredInfo())
            .Where(info => info.RoleId >= RoleId.VIP)
            .ToList();

        if (vips.Count == 0)
        {
            player.SendClientMessage(Color.Red, Messages.NoVIPsConnected);
            return;
        }

        var content = new StringBuilder();
        Color vipColor = Color.Yellow;

        foreach (PlayerInfo vip in vips)
        {
            content.AppendLine($"{vipColor}[VIP] {Color.White}{vip.Name}");
        }

        var dialog = new MessageDialog(
            caption: $"VIPs: {vips.Count}",
            content: content.ToString(),
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }
}
