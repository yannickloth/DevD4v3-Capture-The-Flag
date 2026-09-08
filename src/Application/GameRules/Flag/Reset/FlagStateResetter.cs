namespace CTF.Application.GameRules.TimerNsNs2;

/// <summary>
/// Resets the state of both teams' flags and associated pickups, icons, and timers.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): teamPickupService -> CD-37; teamIconService -> CD-38; flagAutoReturnTimer -> CD-02. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Timer)]
public class FlagStateResetter(
    TeamPickupService teamPickupService,
    TeamIconService teamIconService,
    FlagAutoReturnTimer flagAutoReturnTimer)
{
    /// <summary>Resets both teams' flag state.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.MapIcon, ChangeDriver.Timer)]
    public void Reset(Team firstTeam, Team secondTeam)
    {
        firstTeam.Flag.Reset();
        secondTeam.Flag.Reset();

        teamPickupService.DestroyAllPickups();
        teamPickupService.CreateFlagFromBasePosition(firstTeam);
        teamPickupService.CreateFlagFromBasePosition(secondTeam);

        teamIconService.DestroyAll();
        teamIconService.CreateFromBasePosition(firstTeam);
        teamIconService.CreateFromBasePosition(secondTeam);

        flagAutoReturnTimer.Stop(firstTeam);
        flagAutoReturnTimer.Stop(secondTeam);
    }
}
