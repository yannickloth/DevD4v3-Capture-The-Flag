namespace CTF.Application.Discord;

/// <summary>
/// Notifies an external Discord webhook of player connect/disconnect activity.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): discordWebhookClient -> CD-24. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.Discord, ChangeDriver.Player, ChangeDriver.Ecs)]
public class PlayerActivityNotificationSystem(
    IDiscordWebhookClient discordWebhookClient) : ISystem
{
    /// <summary>Notifies the webhook that a player connected.</summary>
    [ChangeDriversAttribute(ChangeDriver.Discord, ChangeDriver.Player, ChangeDriver.Ecs)]
    [Event]
    public async Task OnPlayerConnect(Player player)
    {
        var content = Smart.Format(Messages.PlayerConnected, new { player.Name });
        await discordWebhookClient.SendAsync(new DiscordMessage(content));
    }

    /// <summary>Notifies the webhook that a player disconnected.</summary>
    [ChangeDriversAttribute(ChangeDriver.Discord, ChangeDriver.Player, ChangeDriver.Ecs)]
    [Event]
    public async Task OnPlayerDisconnect(Player player, DisconnectReason _)
    {
        var content = Smart.Format(Messages.PlayerDisconnected, new { player.Name });
        await discordWebhookClient.SendAsync(new DiscordMessage(content));
    }
}
