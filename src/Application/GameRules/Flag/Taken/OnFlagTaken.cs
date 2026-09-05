namespace CTF.Application.GameRules.Flag.Taken;

/// <summary>
/// This event occurs when a player has taken the flag from a position other than the base.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; teamPickupService -> CD-37; flagAutoReturnTimer -> CD-02; flagCarrierSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Configuration)]
public class OnFlagTaken(
    IWorldService worldService,
    TeamPickupService teamPickupService,
    FlagAutoReturnTimer flagAutoReturnTimer,
    FlagCarrierSettings flagCarrierSettings) : IFlagEvent
{
    /// <summary>Gets the flag status handled by this event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules)]
    public FlagStatus FlagStatus => FlagStatus.Taken;

    /// <summary>Handles the flag-taken event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Configuration, ChangeDriver.MapIcon)]
    public void Handle(Team team, Player player)
    {
        teamPickupService.DestroyFlag(team);
        team.Sounds.PlayFlagTakenSound();
        flagAutoReturnTimer.Stop(team);
        var message = Smart.Format(Messages.OnFlagTaken, new
        {
            PlayerName = player.Name,
            TeamName = team.Name,
            team.ColorName
        });
        worldService.SendClientMessage(team.ColorHex, message);
        worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag taken!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        if (flagCarrierSettings.ShowOnRadarMap)
        {
            player.ShowOnRadarMap();
        }
    }
}
