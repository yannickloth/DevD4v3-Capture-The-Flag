namespace CTF.Host;

/// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-17 (game configuration/.env schema)</remarks>
/// <remarks>Injected dependencies (change drivers of these elements): serverService -> CD-01; serverSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
public class GameModeInit(
    IServerService serverService,
    ServerSettings serverSettings) : ISystem
{
    /// <remarks>Change drivers: CD-01 (open.mp/SampSharp platform API), CD-17 (game configuration/.env schema)</remarks>
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
