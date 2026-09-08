using System.Net.Http.Json;

namespace CTF.Application.Discord.LoggingDomain;

[ChangeDriversAttribute(ChangeDriver.Discord, ChangeDriver.Logging)]
public class DiscordWebhookClient : IDiscordWebhookClient
{
    [ChangeDriversAttribute(ChangeDriver.Discord)]
    private readonly ILogger<DiscordWebhookClient> _logger;
    [ChangeDriversAttribute(ChangeDriver.Discord)]
    private readonly HttpClient _httpClient;
    [ChangeDriversAttribute(ChangeDriver.Discord)]
    private readonly string _discordWebhookUrl;

    [ChangeDriversAttribute(ChangeDriver.Discord)]
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

    [ChangeDriversAttribute(ChangeDriver.Discord)]
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
