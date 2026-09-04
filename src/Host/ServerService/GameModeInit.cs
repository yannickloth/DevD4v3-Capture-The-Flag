namespace CTF.Host.ServerService;

/// <remarks>Change drivers: CD-42 (root; server service API); CD-17 (game configuration/.env schema) → CD-42</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): serverService -> CD-42; serverSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class GameModeInit(
    IServerService serverService,
    ServerSettings serverSettings) : ISystem
{
    /// <remarks>Change drivers: CD-42 (root; server service API); CD-17 (game configuration/.env schema) → CD-42</remarks>
    [Event]
    public void OnGameModeInit()
    {
        Console.WriteLine("\n----------------------------------");
        Console.WriteLine("       Red vs Blue");
        Console.WriteLine("    Capture the Flag");
        Console.WriteLine("----------------------------------\n");

        serverService.SetServerName(serverSettings.HostName);
        serverService.SetLanguage(serverSettings.LanguageText);
        serverService.SetWebsiteUrl(serverSettings.WebUrl);
        serverService.SetGameModeText(serverSettings.GameModeText);
        serverService.UsePlayerPedAnims();
        serverService.DisableInteriorEnterExits();
    }
}
