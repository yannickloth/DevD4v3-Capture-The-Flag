namespace CTF.Application.Teams.Flags;

/// <summary>
/// Resets the state of both teams' flags and associated pickups, icons, and timers.
/// </summary>
/// <remarks>Change drivers: CD-02 (CTF game-rules specification: round/flag reset rule), CD-01 (open.mp/SampSharp platform API: pickups, map icons, timers).</remarks>
public class FlagStateResetter(
    TeamPickupService teamPickupService,
    TeamIconService teamIconService,
    FlagAutoReturnTimer flagAutoReturnTimer)
{
    /// <summary>Resets both teams' flag state.</summary>
    /// <remarks>Change drivers: CD-02 (CTF game-rules specification: round/flag reset rule), CD-01 (open.mp/SampSharp platform API: pickups, map icons, timers).</remarks>
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
