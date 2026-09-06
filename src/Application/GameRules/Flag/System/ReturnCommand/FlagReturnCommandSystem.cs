namespace CTF.Application.GameRules.Flag.System.ReturnCommand;

/// <summary>
/// Returns a flag to its base position via the returnflag command.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; teamPickupService -> CD-37; flagAutoReturnTimer -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer)]
public class FlagReturnCommandSystem(
    IWorldService worldService,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer) : ISystem
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.CommandSet, ChangeDriver.Authorization, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer)]
    [PlayerCommand("returnflag")]
    [RequiresMinimumRole(RoleId.Moderator)]
    public void ReturnToBasePosition(
        Player player,
        [CommandParameter(Name = "red/blue")]string color)
    {
        Team team = color.ToLower() switch
        {
            "red" => Team.Alpha,
            "blue" => Team.Beta,
            _ => null
        };

        if (team is null)
        {
            player.SendClientMessage(Color.Red, Messages.InvalidFlagColor);
            return;
        }

        var message = Smart.Format(Messages.ReturnFlagToBasePosition, new
        {
            PlayerName = player.Name,
            team.ColorName
        });

        team.Flag.Carrier?.Player.HideOnRadarMap();
        team.Flag.ReturnToBase();
        teamPickupService.CreateFlagFromBasePosition(team);
        teamPickupService.DestroyExteriorMarker(team);
        team.Sounds.PlayFlagReturnedSound();
        flagAutoReturnTimer.Stop(team);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag returned!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        worldService.SendClientMessage(Color.Yellow, message);
    }
}
