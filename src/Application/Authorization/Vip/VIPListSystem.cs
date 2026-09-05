namespace CTF.Application.Authorization.Vip;

/// <remarks>Injected dependencies (change drivers of these elements): entityManager -> CD-32; dialogService -> CD-33. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.CommandSet, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure)]
public class VIPListSystem(
    IEntityManager entityManager,
    IDialogService dialogService) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.Authorization, ChangeDriver.Ecs, ChangeDriver.Dialog, ChangeDriver.ClientMessage, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet)]
    [PlayerCommand("vips")]
    public void Show(Player player)
    {
        List<PlayerInfo> vips = entityManager
            .GetComponents<Player>()
            .Select(player => player.GetRequiredInfo())
            .Where(info => info.Role.Id >= RoleId.VIP)
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
            content.AppendLine($"{vipColor}[VIP] {Color.White}{vip.Account.Name}");
        }

        var dialog = new MessageDialog(
            caption: $"VIPs: {vips.Count}",
            content: content.ToString(),
            button1: "Close"
        );

        dialogService.ShowAsync(player, dialog);
    }
}
