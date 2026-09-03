using System.Net.Http.Json;

namespace CTF.Host.Discord;

/// <remarks>Change drivers: CD-24 (root; Discord webhook contract)</remarks>
public class DiscordWebhookClient : IDiscordWebhookClient
{
    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract); CD-23 (Serilog logging) → CD-24</remarks>
    private readonly ILogger<DiscordWebhookClient> _logger;
    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract)</remarks>
    private readonly HttpClient _httpClient;
    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract: the <c>DISCORD_WEBHOOK_URL</c> env variable is anchored under the Discord contract's amendment unit, not the general .env schema)</remarks>
    private readonly string _discordWebhookUrl;

    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract)</remarks>
    private record DiscordWebhookPayload(string Content);

    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract)</remarks>
    public DiscordWebhookClient(
        HttpClient httpClient,
        ILogger<DiscordWebhookClient> logger)
    {
        var envReader = new EnvReader();
        if (!envReader.TryGetStringValue("DISCORD_WEBHOOK_URL", out var webhookUrl))
        {
            logger.LogWarning("Environment variable 'DISCORD_WEBHOOK_URL' is not configured. " +
                "Discord notifications will be disabled.");
        }

        _discordWebhookUrl = webhookUrl ?? string.Empty;
        _logger = logger;
        _httpClient = httpClient;
    }

    /// <remarks>Change drivers: CD-24 (root; Discord webhook contract)</remarks>
    public async Task<bool> SendAsync(DiscordMessage message)
    {
        if (string.IsNullOrWhiteSpace(_discordWebhookUrl))
            return false;

        try
        {
            var payload = new DiscordWebhookPayload(message.Content);
            var response = await _httpClient.PostAsJsonAsync(_discordWebhookUrl, payload);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex) when (
            ex is TaskCanceledException or OperationCanceledException)
        {
            _logger.LogError(ex, "Discord webhook request timed out.");
            return false;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Discord webhook request failed.");
            return false;
        }
    }
}
