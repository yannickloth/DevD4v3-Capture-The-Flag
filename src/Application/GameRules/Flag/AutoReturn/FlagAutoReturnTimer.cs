namespace CTF.Application.GameRules.ConfigurationNsNs3;

/// <summary>
/// A timer service that automatically returns the flag to its base if it is not picked up by a player within a certain time limit.
/// Uniform flow: the CTF flag rules (CD-02) auto-return orchestration plus its engaged
/// timer (CD-41), configuration (CD-17), pickup (CD-37), audio (CD-40) contracts.
/// </summary>
/// <remarks>Injected dependencies (change drivers of these elements): timerService -> CD-41; worldService -> CD-36; teamPickupService -> CD-37; flagAutoReturnSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer, ChangeDriver.Configuration)]
public class FlagAutoReturnTimer(
    ITimerService timerService,
    IWorldService worldService,
    TeamPickupService teamPickupService,
    FlagAutoReturnSettings flagAutoReturnSettings)
{
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer, ChangeDriver.Configuration)]
    private TimerReference _alphaTeamTimer;

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer, ChangeDriver.Configuration)]
    private TimerReference _betaTeamTimer;

    /// <summary>Starts the auto-return timer for the specified team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer, ChangeDriver.Configuration)]
    public void Start(Team team)
    {
        void OnComplete(IServiceProvider serviceProvider)
        {
            teamPickupService.CreateFlagFromBasePosition(team);
            teamPickupService.DestroyExteriorMarker(team);
            team.Sounds.PlayFlagReturnedSound();
            team.Flag.ReturnToBase();
            var message = Smart.Format(Messages.FlagAutoReturn, new
            {
                Seconds = flagAutoReturnSettings.Delay,
                team.ColorName
            });
            worldService.SendClientMessage(team.ColorHex, message);
            worldService.GameText($"~n~~n~~n~{team.GameTextColor}{team.ColorName} flag returned!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);
            Stop(team);
        }

        if (team.Id == TeamId.Alpha)
        {
            TimeSpan interval = TimeSpan.FromSeconds(flagAutoReturnSettings.Delay);
            _alphaTeamTimer ??= timerService.Start(OnComplete, interval);
        }
        else if (team.Id == TeamId.Beta)
        {
            TimeSpan interval = TimeSpan.FromSeconds(flagAutoReturnSettings.Delay);
            _betaTeamTimer ??= timerService.Start(OnComplete, interval);
        }
    }

    /// <summary>Stops the auto-return timer for the specified team.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer, ChangeDriver.Configuration)]
    public void Stop(Team team)
    { 
        if (team.Id == TeamId.Alpha && _alphaTeamTimer is not null) 
        {
            timerService.Stop(_alphaTeamTimer);
            _alphaTeamTimer = default;
        }
        else if (team.Id == TeamId.Beta && _betaTeamTimer is not null)
        {
            timerService.Stop(_betaTeamTimer);
            _betaTeamTimer = default;
        }
    }
}
