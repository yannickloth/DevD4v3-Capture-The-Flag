namespace CTF.Application.MapRotation.AuthorizationNsNs3;

/// <summary>
/// Provides the moderator command to set the remaining round time.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): mapRotationService -> CD-12; mapTextDrawRenderer -> CD-34. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.CommandInfrastructure, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
public class SetTimeLeftCommand(
    MapRotationService mapRotationService,
    MapTextDrawRenderer mapTextDrawRenderer) : ISystem
{
    [PlayerCommand("settimeleft")]
    [RequiresMinimumRole(RoleId.Moderator)]
    [ChangeDriversAttribute(ChangeDriver.MapRotation, ChangeDriver.CommandInfrastructure, ChangeDriver.TextDraw, ChangeDriver.ClientMessage, ChangeDriver.CommandSet, ChangeDriver.Authorization)]
    public void SetTimeLeft(Player player, int minutes)
    {
        var interval = new Minutes(minutes);
        TimeLeft timeLeft = mapRotationService.TimeLeft;
        Result result = timeLeft.SetInterval(interval);
        if (result.IsFailed)
        {
            player.SendClientMessage(Color.Red, result.Message);
            return;
        }

        mapTextDrawRenderer.UpdateTimeLeft(timeLeft);
    }
}
