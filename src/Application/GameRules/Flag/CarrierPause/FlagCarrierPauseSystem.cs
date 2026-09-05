namespace CTF.Application.GameRules.Flag.CarrierPause;

/// <summary>
/// A system that handles the pause logic for flag carriers.
/// </summary>
/// <remarks>
/// It checks if the carrier is paused and updates the timer. If the timer runs out, the flag is returned to the base.
/// </remarks>
/// <remarks>Injected dependencies (change drivers of these elements): worldService -> CD-36; timerService -> CD-41; teamPickupService -> CD-37; flagCarrierSettings -> CD-17. Each injection parameter is driven by the contract of its injected type + CD-21 (DI wiring).</remarks>
[ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Pickup, ChangeDriver.Audio, ChangeDriver.Timer, ChangeDriver.Player, ChangeDriver.Configuration)]
public class FlagCarrierPauseSystem(
    IWorldService worldService,
    ITimerService timerService,
    TeamPickupService teamPickupService,
    FlagCarrierSettings flagCarrierSettings) : ISystem
{
    /// <summary>Stops the pause timer when a carrier disconnects.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Timer, ChangeDriver.Player)]
    [Event]
    public void OnPlayerDisconnect(Player player, DisconnectReason reason)
    {
        var pauseTimerReference = player.GetComponent<PauseTimerReference>();
        if (pauseTimerReference is null)
            return;

        timerService.Stop(pauseTimerReference.Value);
    }

    /// <summary>Handles the pause state change for flag carriers.</summary>
    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Timer, ChangeDriver.Player, ChangeDriver.Configuration)]
    [Event]
    public void OnPlayerPauseStateChange(Player player, bool pauseState)
    {
        PlayerInfo playerInfo = player.GetRequiredInfo();
        if (pauseState && playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
        {
            var interval = TimeSpan.FromSeconds(flagCarrierSettings.PauseTime);
            var timerReference = timerService.Start(OnComplete, interval);
            player.AddComponent<PauseTimerReference>(timerReference);
        }
        else if (!pauseState)
        {
            var pauseTimerReference = player.GetComponent<PauseTimerReference>();
            if (pauseTimerReference is null)
                return;

            timerService.Stop(pauseTimerReference.Value);
            pauseTimerReference.Destroy();
        }

        void OnComplete(IServiceProvider serviceProvider)
        {
            if (!player.IsComponentAlive)
                return;

            var pauseTimerReference = player.GetComponent<PauseTimerReference>();
            timerService.Stop(pauseTimerReference.Value);
            pauseTimerReference.Destroy();

            if (!playerInfo.Appearance.Team.RivalTeam.Flag.IsCarriedBy(player))
                return;

            Team rivalTeam = playerInfo.Appearance.Team.RivalTeam;
            rivalTeam.Flag.ReturnToBase();
            player.HideOnRadarMap();
            teamPickupService.CreateFlagFromBasePosition(rivalTeam);
            teamPickupService.DestroyExteriorMarker(rivalTeam);
            rivalTeam.Sounds.PlayFlagReturnedSound();
            var message = Smart.Format(Messages.FlagAutoReturn2, new
            {
                rivalTeam.ColorName,
                PlayerName = player.Name,
                Seconds = flagCarrierSettings.PauseTime
            });
            worldService.SendClientMessage(rivalTeam.ColorHex, message);
            worldService.GameText($"~n~~n~~n~{rivalTeam.GameTextColor}{rivalTeam.ColorName} flag returned!", TimeSpan.FromSeconds(5), GameTextStyle.Style3);
        }
    }

    [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Configuration, ChangeDriver.Timer)]
    private class PauseTimerReference(TimerReference value) : Component
    {
        [ChangeDriversAttribute(ChangeDriver.GameRules, ChangeDriver.Timer)]
        public TimerReference Value { get; } = value;
    }
}
