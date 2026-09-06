namespace CTF.Application.MapRotation.Commands.Spawn;

/// <summary>
/// Shows the current map textdraw to a player on spawn.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): mapTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Player, ChangeDriver.TextDraw)]
public class MapRotationSpawnSystem(
    MapTextDrawRenderer mapTextDrawRenderer) : ISystem
{
    [Event]
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.Player, ChangeDriver.TextDraw)]
    public void OnPlayerSpawn(Player player)
    {
        mapTextDrawRenderer.Show(player);
    }
}
