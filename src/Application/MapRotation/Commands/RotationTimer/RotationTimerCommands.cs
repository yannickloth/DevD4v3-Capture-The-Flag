namespace CTF.Application.MapRotation.Commands.RotationTimer;

/// <summary>
/// Provides the moderator commands to start and stop the map-rotation timer.
/// </summary>
/// <remarks>Injected dependency (change driver of this element): mapRotationService -> CD-12. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
public class RotationTimerCommands(
    MapRotationService mapRotationService) : ISystem
{
    [PlayerCommand("startrt")]
    [RequiresMinimumRole(RoleId.Moderator)]
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
    public void StartRotationTimer(Player player)
    {
        mapRotationService.StartRotationTimer();
    }

    [PlayerCommand("stoprt")]
    [RequiresMinimumRole(RoleId.Moderator)]
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.CommandInfrastructure, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
    public void StopRotationTimer(Player player)
    {
        mapRotationService.StopRotationTimer();
    }
}
