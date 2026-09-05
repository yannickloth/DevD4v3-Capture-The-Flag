namespace CTF.Host.ServerService;

/// <remarks>Injected dependencies (change drivers of these elements): serverService -> CD-42; serverSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.ServerService, ChangeDriver.Configuration)]
public class GameModeInit(
    IServerService serverService,
    ServerSettings serverSettings) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.ServerService, ChangeDriver.Configuration)]
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
