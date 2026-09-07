namespace CTF.Application.GameRules.Flag.Taken;

/// <summary>
/// This event occurs when a player has taken the flag from a position other than the base.
/// Uniform flow: the CTF flag rules (CD-02) plus the engaged contracts of the take
/// handling (pickup CD-37, map icon CD-38, audio CD-40, GameText CD-35,
/// client-message CD-36, configuration CD-17).
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
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Configuration)]
    public FlagStatus FlagStatus => FlagStatus.Taken;

    /// <summary>Handles the flag-taken event.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.GameText, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Audio, ChangeDriver.Configuration)]
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
