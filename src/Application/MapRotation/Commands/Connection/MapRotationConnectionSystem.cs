namespace CTF.Application.MapRotation.Commands.Connection;

/// <summary>
/// Starts and stops the map-rotation timer as players connect and disconnect.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): mapRotationService -> CD-12. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Player)]
public class MapRotationConnectionSystem(
    MapRotationService mapRotationService) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.MapRotation)]
    private int _connectedPlayers;

    [Event]
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Player)]
    public void OnPlayerConnect(Player player)
    {
        _connectedPlayers++;
        if (_connectedPlayers == 1)
            mapRotationService.StartRotationTimer();
    }

    [Event]
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Player)]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        _connectedPlayers--;
        if (_connectedPlayers == 0)
            mapRotationService.StopRotationTimer();
    }
}
