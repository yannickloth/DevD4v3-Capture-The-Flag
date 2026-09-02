namespace CTF.Application.Players;

/// <summary>
/// Notifies an external Discord webhook of player connect/disconnect activity.
/// </summary>
/// <remarks>Change drivers: CD-24 (root; Discord webhook contract); CD-01 (open.mp/SampSharp platform API) → CD-24</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): discordWebhookClient -> CD-24. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class PlayerActivityNotificationSystem(
    IDiscordWebhookClient discordWebhookClient) : ISystem
{
    /// <summary>Notifies the webhook that a player connected.</summary>
    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract); CD-01 (open.mp/SampSharp platform API) → CD-24</remarks>
    [Event]
    public async Task OnPlayerConnect(Player player)
    {
        var content = Smart.Format(Messages.PlayerConnected, new { player.Name });
        await discordWebhookClient.SendAsync(new DiscordMessage(content));
    }

    /// <summary>Notifies the webhook that a player disconnected.</summary>
    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract); CD-01 (open.mp/SampSharp platform API) → CD-24</remarks>
    [Event]
    public async Task OnPlayerDisconnect(Player player, DisconnectReason _)
    {
        var content = Smart.Format(Messages.PlayerDisconnected, new { player.Name });
        await discordWebhookClient.SendAsync(new DiscordMessage(content));
    }
}
